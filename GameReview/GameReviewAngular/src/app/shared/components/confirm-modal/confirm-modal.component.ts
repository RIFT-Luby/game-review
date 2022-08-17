import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-modal',
  templateUrl: './confirm-modal.component.html',
  styleUrls: ['./confirm-modal.component.scss']
})
export class ConfirmModalComponent implements OnInit {

  constructor(
    private matDialogRef: MatDialogRef<ConfirmModalComponent>,
  ) { }

  confirm(confirmed: boolean): void {
    this.matDialogRef.close(confirmed);
  }

  ngOnInit(): void {

  }
}
