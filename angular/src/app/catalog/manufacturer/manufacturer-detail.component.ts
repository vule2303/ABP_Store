import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { attributeTypeOptions } from '@proxy/store/product-attributes';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { NotificationService } from '../../shared/services/notification.service';
import { UtilityService } from '../../shared/services/utility.service';
import { ProductCategoriesService, ProductCategoryDto } from '@proxy/product-categories';
import { DomSanitizer } from '@angular/platform-browser';
import { ManufacturerDto, ManufacturersService } from '@proxy/manufacturers';

@Component({
  selector: 'app-manufacturer-detail',
  templateUrl: './manufacturer-detail.component.html',
})
export class ManufacturerDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  btnDisabled = false;
  public form: FormGroup;
  public coverPicture;
  //Dropdown
  dataTypes: any[] = [];
  selectedEntity = {} as ManufacturerDto;

  constructor(
    private manufacturerService: ManufacturersService,
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
      this.getNewSuggestionCode();
    } else {
      this.loadFormDetails(this.config.data?.id);
    }
  }

  loadFormDetails(id: string) {
    this.toggleBlockUI(true);
    this.manufacturerService
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
      this.manufacturerService
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
      this.manufacturerService
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
      slug: new FormControl(this.selectedEntity.slug || null, Validators.required),
      country: new FormControl(this.selectedEntity.country || null, Validators.required),
      visibility: new FormControl(this.selectedEntity.visibility || false),
      isActive: new FormControl(this.selectedEntity.isActive || false),
      coverPictureName: new FormControl(this.selectedEntity.coverPicture || null),
      coverPictureContent: new FormControl(null),
    });

  }
  //loadthumb nail picture
  loadThumbnail(filename: string) {
    this.manufacturerService.getThumbnailImage(filename)
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
    this.manufacturerService
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