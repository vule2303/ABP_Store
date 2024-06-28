import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { attributeTypeOptions } from '@proxy/store/product-attributes';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { NotificationService } from '../../shared/services/notification.service';
import { UtilityService } from '../../shared/services/utility.service';
import { ProductCategoriesService, ProductCategoryDto } from '@proxy/product-categories';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-category-detail',
  templateUrl: './category-detail.component.html',
})
export class CategoryDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  btnDisabled = false;
  public form: FormGroup;
  public coverPicture;
  //Dropdown
  dataTypes: any[] = [];
  selectedEntity = {} as ProductCategoryDto;

  constructor(
    private categoryService: ProductCategoriesService,
    private fb: FormBuilder,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private utilService: UtilityService,
    private notificationSerivce: NotificationService,
    private sanitizer: DomSanitizer,
    private cd : ChangeDetectorRef
  ) {}

  validationMessages = {
    code: [{ type: 'required', message: 'Bạn phải nhập mã duy nhất' }],
    name: [
      { type: 'required', message: 'Bạn phải nhập nhãn hiển thị' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    sortOrder: [{ type: 'required', message: 'Bạn phải nhập thứ tự' }],
  };

  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.buildForm();
    this.loadAttributeTypes();
    this.initFormData();
  }

  initFormData() {
    //Load edit data to form
    if (this.utilService.isEmpty(this.config.data?.id) == true) {
      this.toggleBlockUI(false);
    } else {
      this.loadFormDetails(this.config.data?.id);
    }
  }

  loadFormDetails(id: string) {
    this.toggleBlockUI(true);
    this.categoryService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: ProductCategoryDto) => {
          this.selectedEntity = response;
          this.loadThumbnail(this.selectedEntity.coverPicture);
          this.buildForm();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  saveChange() {
    this.toggleBlockUI(true);

    if (this.utilService.isEmpty(this.config.data?.id) == true) {
      this.categoryService
        .create(this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: () => {
            this.toggleBlockUI(false);
            this.ref.close(this.form.value);
          },
          error: err => {
            this.notificationSerivce.showError(err.error.error.message);
            this.toggleBlockUI(false);
          },
        });
    } else {
      this.categoryService
        .update(this.config.data?.id, this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: () => {
            this.toggleBlockUI(false);
            this.ref.close(this.form.value);
          },
          error: err => {
            this.notificationSerivce.showError(err.error.error.message);
            this.toggleBlockUI(false);
          },
        });
    }
  }

  loadAttributeTypes() {
    attributeTypeOptions.forEach(element => {
      this.dataTypes.push({
        value: element.value,
        label: element.key,
      });
    });
  }

  private buildForm() {
    this.form = this.fb.group({
      name: new FormControl(
        this.selectedEntity.name || null,
        Validators.compose([Validators.required, Validators.maxLength(250)])
      ),
      code: new FormControl(this.selectedEntity.code || null, Validators.required),
      sortOrder: new FormControl(this.selectedEntity.sortOrder || null, Validators.required),
      slug: new FormControl(this.selectedEntity.slug || null, Validators.required),
      visibility: new FormControl(this.selectedEntity.visibility || true),
      isActive: new FormControl(this.selectedEntity.isActive || true),
      seoMetaDescription: new FormControl(this.selectedEntity.seoMetaDescription || null),
      coverPictureName: new FormControl(this.selectedEntity.coverPicture || null),
      coverPictureContent: new FormControl(null),
    });

  }
  //load thumbnail picture
  loadThumbnail(filename: string) {
    this.categoryService.getThumbnailImage(filename)
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: (response: string) => {
        var fileExt = this.selectedEntity.coverPicture.split('.').pop();
        this.coverPicture = this.sanitizer.bypassSecurityTrustResourceUrl(
          `data:image/${fileExt};base64,${response}`
        );
      },
    })
  }
  //get new suggestion code
  getNewSuggestionCode() {
    this.categoryService
      .getSuggestNewCode()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: string) => {
          this.form.patchValue({
            code: response,
          });
        }
      });
  }
  generateSlug() {
    this.form.controls['slug'].setValue(this.utilService.MakeSeoTitle(this.form.get('name').value));
  }
  onFileChange(event){
    const reader = new FileReader();

    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.form.patchValue({
          coverPictureName: file.name,
          coverPictureContent: reader.result,
        });
        // need to run CD since file load runs outside of zone
        this.cd.markForCheck();
      };
    }
  }
  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true;
      this.btnDisabled = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
        this.btnDisabled = false;
      }, 1000);
    }
  }

}