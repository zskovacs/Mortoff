import { ChangeDetectorRef, Component, ElementRef, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FileValidator } from '../../shared/file.validator';
import { ErrorResponse, StockClient } from '../mortoff-api';
import { BsModalService, BsModalRef, ModalOptions } from 'ngx-bootstrap/modal';
import { AlertModalContentComponent } from '../../shared/alert-modal/alert-modal.component';

@Component({
  selector: 'app-stock-add-data',
  templateUrl: './stock-add-data.component.html',
  styleUrls: ['./stock-add-data.component.scss']
})
export class StockAddDataComponent implements OnInit {
  @ViewChild('alertBox') alertBox: ElementRef;

  constructor(private _formBuilder: FormBuilder, private cd: ChangeDetectorRef, private _stockClient: StockClient, private _modalService: BsModalService) {

    const allowedExtensions = ['csv', 'xls'];

    this.mainForm = this._formBuilder.group({
      name: ['', [Validators.required, Validators.maxLength(40), Validators.pattern('^[A-Za-z0-9 ]*$')]],
      file: [null, [Validators.required, FileValidator.fileMaxSize(1024000), FileValidator.fileExtensions(allowedExtensions)]]
    });
  }


  modalRef?: BsModalRef;
  mainForm: FormGroup;
  showAlert: boolean;
  showSuccess: boolean;

  ngOnInit(): void {
    this.showAlert = false;
  }

  onFileChange($event: any) {
    if ($event.target.files.length > 0) {
      this.mainForm.patchValue({
        file: $event.target.files[0]
      });
      this.cd.markForCheck();
    }
  }

  submit() {

    this.mainForm.disable();
    const me = this;

    this._stockClient.checkStock(this.mainForm.get('name')?.value).subscribe({
      next: (value: boolean) => {
        if (value)
          this.modalRef = this._modalService.show(this.alertBox as any);
      },      
      error: (e: ErrorResponse) => {
        this.mainForm.enable();
        me.showAlert = true;
        setTimeout(function () {
          me.showAlert = false;
        }, 3000);
      }
    });
  }

  confirm(): void {
    this.save();
    this.modalRef?.hide();
  }

  decline(): void {
    this.mainForm.enable();
    this.modalRef?.hide();
  }

  private save(): void {
    const me = this;

    var file = { data: this.mainForm.get('file')?.value, fileName: this.mainForm.get('file')?.value.name }
    this._stockClient.uploadStockData(file, this.mainForm.get('name')?.value).subscribe({
      complete: () => {
        this.mainForm.enable();
        me.showSuccess = true;
        setTimeout(function () {
          me.showSuccess = false;
        }, 3000);
      },
      error: (e: ErrorResponse) => {
        this.mainForm.enable();
        me.showAlert = true;
        setTimeout(function () {
          me.showAlert = false;
        }, 3000);
      }
    });
  }
}
