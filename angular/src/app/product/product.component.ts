import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProductCategoriesService, ProductCategoryInListDto } from '@proxy/product-categories';
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

  //Filter
  productCategories: any[] = [];
  keyword : string = '';
  categoryId: string = '';

  constructor(private productService: ProductsService, private productCategoryService: ProductCategoriesService) {}
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.loadData();
    this.loadProductCategories();
  }

  loadData(){
    this.toggleBlockUI(true);
    this.productService.getListFilter({
      keyword: this.keyword,
      categoryId: this.categoryId,
      maxResultCount: this.maxResultCount,
      skipCount: this.skipCount,
    })
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: (res: PagedResultDto<ProductInListDto>) => {
        this.items = res.items;
        this.totalCount = res.totalCount;
        this.toggleBlockUI(false);
      },
      error: () => {
        this.toggleBlockUI(false);
      }
    });
  }

  loadProductCategories(){
    this.productCategoryService.getListAll()
    .subscribe((response: ProductCategoryInListDto[]) => {
      response.forEach(item => {
        this.productCategories.push({
          id: item.id,
          name: item.name
        });
      })
    })
  }

  pageChanged(event: any): void {
    this.skipCount = (event.page - 1) * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }

  private toggleBlockUI(enabled: boolean) {
    enabled ? this.blockedPanel = true : setTimeout(() => {
      this.blockedPanel = false;
    })
  }

}
