import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { AttributeType } from '@proxy/store/product-attributes';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, take, takeUntil } from 'rxjs';
import { NotificationService } from '../../shared/services/notification.service';
import { ManufacturerDetailComponent } from './manufacturer-detail.component';
import { ProductCategoriesService, ProductCategoryDto, ProductCategoryInListDto } from '@proxy/product-categories';
import { ManufacturerInListDto, ManufacturersService } from '@proxy/manufacturers';

@Component({
  selector: 'app-category',
  templateUrl: './manufacturer.component.html',
  styleUrls: ['./manufacturer.component.scss'],
})
export class ManufacturerComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: ManufacturerInListDto[] = [];
  public selectedItems: ManufacturerInListDto[] = [];

  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;

  //Filter
  ProductCategories: any[] = [];
  keyword: string = '';
  categoryId: string = '';

  constructor(
    private manufacturerService: ManufacturersService,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.toggleBlockUI(true);
    this.manufacturerService
      .getListFilter({
        keyword: this.keyword,
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
      })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<ProductCategoryInListDto>) => {
          this.items = response.items;
          console.log('items', this.items);
          this.totalCount = response.totalCount;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  pageChanged(event: any): void {
    this.skipCount = (event.page) * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }
  showAddModal() {
    const ref = this.dialogService.open(ManufacturerDetailComponent, {
      header: 'Thêm mới loại sản phẩm',
      width: '70%',
    });

    ref.onClose.subscribe((data: ProductCategoryDto) => {
      if (data) {
        this.loadData();
        this.notificationService.showSuccess('Thêm loại sản phẩm thành công');
        this.selectedItems = [];
      }
    });
  }

  showEditModal() {
    if (this.selectedItems.length == 0) {
      this.notificationService.showError('Bạn phải chọn một bản ghi');
      return;
    }
    const id = this.selectedItems[0].id;
    const ref = this.dialogService.open(ManufacturerDetailComponent, {
      data: {
        id: id,
      },
      header: 'Cập nhật loại sản phẩm',
      width: '70%',
    });

    ref.onClose.subscribe((data: ProductCategoryDto) => {
      if (data) {
        this.loadData();
        this.selectedItems = [];
        this.notificationService.showSuccess('Cập nhật loại sản phẩm thành công');
      }
    });
  }
  //delete Item
  deleteItems(){
    if(this.selectedItems.length == 0){
      this.notificationService.showError("Phải chọn ít nhất một bản ghi");
      return;
    }
    var ids =[];
    this.selectedItems.forEach(element=>{
      ids.push(element.id);
    });
    this.confirmationService.confirm({
      message:'Bạn có chắc muốn xóa bản ghi này?',
      accept:()=>{
        this.deleteItemsConfirmed(ids);
      }
    })
  }

  deleteItemsConfirmed(ids: string[]){
    this.toggleBlockUI(true);
    this.manufacturerService.deleteMultiple(ids).pipe(takeUntil(this.ngUnsubscribe)).subscribe({
      next: ()=>{
        this.notificationService.showSuccess("Xóa thành công");
        this.loadData();
        this.selectedItems = [];
        this.toggleBlockUI(false);
      },
      error:()=>{
        this.toggleBlockUI(false);
      }
    })
  }

  getAttributeTypeName(value: number){
    return AttributeType[value];
  }

  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 1000);
    }
  }
}