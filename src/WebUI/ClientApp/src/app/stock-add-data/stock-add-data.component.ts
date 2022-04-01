import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FileValidator } from '../../shared/file.validator';

@Component({
  selector: 'app-stock-add-data',
  templateUrl: './stock-add-data.component.html',
  styleUrls: ['./stock-add-data.component.scss']
})
export class StockAddDataComponent implements OnInit {

  constructor(private _formBuilder: FormBuilder, private cd: ChangeDetectorRef) {

    const allowedExtensions = ['csv', 'xls'];

    this.mainForm = this._formBuilder.group({
      name: ['', [Validators.required, Validators.maxLength(40)]],
      file: [null, [Validators.required, FileValidator.fileMaxSize(1024000), FileValidator.fileExtensions(allowedExtensions)]]
    });
  }

  public get form(): { [key: string]: AbstractControl } {
    return this.mainForm.controls;
  }

  mainForm: FormGroup;

  ngOnInit(): void {

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

  }

}
