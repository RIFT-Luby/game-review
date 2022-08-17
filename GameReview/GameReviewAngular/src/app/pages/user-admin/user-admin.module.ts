import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserAdminComponent } from './user-admin.component';
import { MatExpansionModule} from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { UserFormModule } from 'src/app/shared/components/user-form/user-form.module';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    MatExpansionModule,
    MatIconModule,
    MatButtonModule,
    UserFormModule,
    HttpClientModule,
    RouterModule,
  ],
  declarations: [UserAdminComponent],
  exports: [
    UserAdminComponent
  ]
})
export class UserAdminModule { }
