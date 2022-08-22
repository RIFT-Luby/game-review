import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameAdminComponent } from './game-admin.component';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatOptionModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { GameAdminFormModule } from './game-admin-form/game-admin-form.module';

@NgModule({
  imports: [
    CommonModule,
    GameAdminFormModule,
    RouterModule,
    FormsModule,
    MatTableModule,
    ReactiveFormsModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatOptionModule,
    MatButtonModule,
    MatIconModule
  ],
  declarations: [GameAdminComponent]
})
export class GameAdminModule { }
