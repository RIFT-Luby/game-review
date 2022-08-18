import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-loading-modal',
  templateUrl: './loading-modal.component.html',
  styleUrls: ['./loading-modal.component.scss']
})
export class LoadingModalComponent implements OnInit {

  constructor(
    private matDialogRef: MatDialogRef<LoadingModalComponent>,
  ) { }

  confirm(confirmed: boolean): void {
    this.matDialogRef.close(confirmed);
  }

  ngOnInit(): void {
  }

}
