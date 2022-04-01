import { Component, OnInit } from "@angular/core";
import { BsModalRef } from "ngx-bootstrap/modal";

@Component({
  // eslint-disable-next-line @angular-eslint/component-selector
  selector: 'alert-modal',
  templateUrl: './alert-modal.component.html',
})

export class AlertModalContentComponent implements OnInit {
  title?: string;
  message?: string;

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {
    
  }
}
