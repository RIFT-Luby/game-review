import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UserAdminModule } from './pages/user-admin/user-admin.module';
import { HttpClientModule } from '@angular/common/http';
import { UserFormComponent } from './shared/components/user-form/user-form.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    UserAdminModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
