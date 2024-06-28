import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { ProductComponent } from './product/product.component';
import { PanelModule } from 'primeng/panel';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { ProductDetailComponent } from './product/product-detail.component';
import { InputNumberModule } from 'primeng/inputnumber';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { EditorModule } from 'primeng/editor';
import { StoreSharedModule } from '../shared/modules/store-shared.module';
import { BadgeModule } from 'primeng/badge';
import { ImageModule } from 'primeng/image';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ProductAttributeComponent } from './product/product-attribute.component';
import { CalendarModule } from 'primeng/calendar';
import { CatalogRoutingModule } from './catalog-routing.module';
import { AttributeComponent } from './attribute/attribute.component';
import { AttributeDetailComponent } from './attribute/attribute-detail.component';
import { CategoryComponent } from './category/category.component';
import { CategoryDetailComponent } from './category/category-detail.component';
import { ManufacturerComponent } from './manufacturer/manufacturer.component';
import { ManufacturerDetailComponent } from './manufacturer/manufacturer-detail.component';
@NgModule({
  declarations: [
    ProductComponent,
    ProductDetailComponent,
    ProductAttributeComponent,
    AttributeComponent,
    AttributeDetailComponent,
    CategoryComponent,
    CategoryDetailComponent,
    ManufacturerComponent,
    ManufacturerDetailComponent
  ],
  imports: [
    SharedModule,
    CatalogRoutingModule,
    PanelModule,
    TableModule,
    PaginatorModule,
    BlockUIModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    ProgressSpinnerModule,
    DynamicDialogModule,
    InputNumberModule,
    CheckboxModule,
    InputTextareaModule,
    EditorModule,
    StoreSharedModule,
    BadgeModule,
    ImageModule,
    ConfirmDialogModule,
    CalendarModule,
  ],
  entryComponents: [ProductDetailComponent, ProductAttributeComponent, AttributeDetailComponent, CategoryComponent, CategoryDetailComponent, ManufacturerComponent, ManufacturerDetailComponent],
})
export class CatalogModule {}