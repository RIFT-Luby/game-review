import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserAdminComponent } from './user-admin.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [UserAdminComponent],
  exports: [
    UserAdminComponent
  ]
})
export class UserAdminModule { }
