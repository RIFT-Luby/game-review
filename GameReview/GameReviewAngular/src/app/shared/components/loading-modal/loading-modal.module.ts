import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingModalComponent } from './loading-modal.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { MatDialogModule } from '@angular/material/dialog';


@NgModule({
  declarations: [
    LoadingModalComponent
  ],
  imports: [
    CommonModule,
    MatProgressSpinnerModule,
    MatDialogModule
  ],
  exports: [
    LoadingModalComponent
  ]
})
export class LoadingModalModule { }
