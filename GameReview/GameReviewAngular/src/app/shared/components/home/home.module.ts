import { RouterModule } from '@angular/router';
import { NavbarModule } from './../navbar/navbar.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    NavbarModule,
    RouterModule
  ],
  exports:[
    HomeComponent
  ]
})
export class HomeModule { }
