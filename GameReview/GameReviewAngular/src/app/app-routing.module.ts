import { ReviewComponent } from './pages/review/review.component';

import { AuthGuard } from './../../src - Copia/app/shared/guards/auth.guard';
import { LoginComponent } from './pages/login/login.component';
import { ApiHttpInterceptor } from './shared/interceptors/api-http.intercerptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './shared/components/home/home.component';

const routes: Routes = [
  {path: 'home', component: HomeComponent, canActivate: [AuthGuard], children: [
    {path: 'reviews', component: ReviewComponent, canActivate: [AuthGuard]}
    ],
  },
  {path: '**', component: LoginComponent},
  {path: 'login', component: LoginComponent}
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
