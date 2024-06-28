import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';

@Component({
  selector: 'app-menu',
  templateUrl: './app.menu.component.html',
})
export class AppMenuComponent implements OnInit {
  model: any[] = [];

  constructor(public layoutService: LayoutService) {}

  ngOnInit() {
    this.model = [
      {
        label: 'Trang chủ',
        items: [{ label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['/'] }],
      },
      {
        label: 'Sản phẩm',
        items: [
          {
            label: 'Danh sách sản phẩm',
            icon: 'pi pi-fw pi-circle',
            routerLink: ['/catalog/product'],
            permission: 'StoreAdminCatalog.Product',
          },
          {
            label: 'Danh sách loại sản phẩm',
            icon: 'pi pi-fw pi-circle',
            routerLink: ['/catalog/category'],
            permission: 'StoreAdminCatalog.ProductCategory',
          },
          {
            label: 'Danh sách nhà cung cấp',
            icon: 'pi pi-fw pi-circle',
            routerLink: ['/catalog/manufacturer'],
            permission: 'StoreAdminCatalog.Manufacturer',
          },
          {
            label: 'Danh sách thuộc tính',
            icon: 'pi pi-fw pi-circle',
            routerLink: ['/catalog/attribute'],
            permission: 'StoreAdminCatalog.Attribute',
          },
        ],
      },
      {
        label: 'Hệ thống',
        items: [
          {
            label: 'Danh sách quyền',
            icon: 'pi pi-fw pi-circle',
            routerLink: ['/system/role'],
            permission: 'AbpIdentity.Roles',
          },
          {
            label: 'Danh sách người dùng',
            icon: 'pi pi-fw pi-circle',
            routerLink: ['/system/user'],
            permission: 'AbpIdentity.Users',
          },
        ],
      },
    ];
  }
}