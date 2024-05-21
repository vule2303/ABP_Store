import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProductInListDto, ProductsService } from '@proxy/products';
import { takeUntil,Subject } from 'rxjs';
@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductComponent implements OnInit, OnDestroy{
  private ngUnsubscribe = new Subject<void>();

  blockedPanel: boolean = false;
  items: ProductInListDto[] = [];
  //pagination variables
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;
  constructor(private productService: ProductsService) {}
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.loadData();
    
  }

  loadData(){
    this.productService.getListFilter({
      keyword: '',
      maxResultCount: this.maxResultCount,
      skipCount: this.skipCount,
    })
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: (res: PagedResultDto<ProductInListDto>) => {
        this.items = res.items;
        this.totalCount = res.totalCount;
      },
      error: () => {}
    });
  }
}
