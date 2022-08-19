import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserPasswordFormComponent } from './user-password-form.component';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    MatButtonModule,
    MatInputModule,
    MatSnackBarModule,
    MatFormFieldModule,
    ReactiveFormsModule
  ],
  declarations: [UserPasswordFormComponent],
  exports: [
    UserPasswordFormComponent
  ]
})
export class UserPasswordFormModule { }
