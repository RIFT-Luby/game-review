import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatButtonModule } from '@angular/material/button';
import { UserFormModule } from 'src/app/shared/components/user-form/user-form.module';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import {MatCardModule} from '@angular/material/card';

@NgModule({
  imports: [
    CommonModule,
    MatExpansionModule,
    MatIconModule,
    MatButtonModule,
    MatCardModule,
    UserFormModule,
    HttpClientModule,
    RouterModule,
  ],
  declarations: [UserComponent]
})
export class UserModule { }
