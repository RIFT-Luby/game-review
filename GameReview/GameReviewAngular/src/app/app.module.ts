import { ConfirmModalModule } from './shared/components/confirm-modal/confirm-modal.module';
import { ReviewAdminModule } from './pages/review-admin/review-admin.module';
import { ReviewFormModule } from './pages/review/review-form/review-form.module';
import { ReviewModule } from './pages/review/review.module';
import { NavbarModule } from './shared/components/navbar/navbar.module';

import { LoginModule } from './pages/login/login.module';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UserAdminModule } from './pages/user-admin/user-admin.module';
import { HttpClientModule } from '@angular/common/http';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { HomeModule } from './shared/components/home/home.module';
import { LoadingModalModule } from './shared/components/loading-modal/loading-modal.module';
import { UserModule } from './pages/user/user.module';
import { RegisterModule } from './pages/register/register.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HomeModule,
    NavbarModule,
    LoginModule,
    RegisterModule,
    ReviewModule,
    ReviewFormModule,
    ReviewAdminModule,
    ConfirmModalModule,
    MatSnackBarModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    UserAdminModule,
    UserModule,
    LoadingModalModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
