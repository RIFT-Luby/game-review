import { ApiHttpInterceptor } from './shared/interceptors/api-http.intercerptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserAdminComponent } from './pages/user-admin/user-admin.component';
import { UserFormComponent } from './shared/components/user-form/user-form.component';

const routes: Routes = [
  {path: 'userAdmin', children: [
    {path: '', component: UserAdminComponent},
    {path: 'form/:id', component: UserFormComponent}
  ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ApiHttpInterceptor,
      multi: true
    },
  ],
})
export class AppRoutingModule { }
