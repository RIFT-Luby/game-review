import { ReviewAdminComponent } from './pages/review-admin/review-admin.component';
import { ReviewFormComponent } from './pages/review/review-form/review-form.component';
import { ReviewComponent } from './pages/review/review.component';
import { LoginComponent } from './pages/login/login.component';
import { ApiHttpInterceptor } from './shared/interceptors/api-http.intercerptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './shared/components/home/home.component';
import { AuthGuard } from './shared/guards/auth.guard';
import { AdminAuth } from './shared/guards/admin-auth.guard';
import { UserComponent } from './pages/user/user.component';
import { RegisterComponent } from './pages/register/register.component';
import { UserAdminComponent } from './pages/user-admin/user-admin.component';
import { UserFormComponent } from './shared/components/user-form/user-form.component';
import { GameAdminComponent } from './pages/game-admin/game-admin.component';
import { GameAdminFormComponent } from './pages/game-admin/game-admin-form/game-admin-form.component';

const routes: Routes = [
  {path: 'home', component: HomeComponent, canActivate: [AuthGuard], children: [
    {path: 'reviews', component: ReviewComponent, canActivate: [AuthGuard]},
    {path: 'reviews/create-review', component: ReviewFormComponent, canActivate: [AuthGuard]},
    {path: 'reviews/create-review/:id', component: ReviewFormComponent, canActivate: [AuthGuard]},
    {path: 'admin',
      canActivate: [AdminAuth],
      children: [
        {path: 'reviews', component: ReviewAdminComponent},
        {path: 'users', children: [
          {path: '', component: UserAdminComponent},
          {path: 'form/:id', component: UserFormComponent}
        ]},
        {path:'games',children:[
          {path:'',component: GameAdminComponent},
          {path:'form',component: GameAdminFormComponent},
          {path:'form/:id',component: GameAdminFormComponent},
        ]}
        ],
    },
    {path: 'user',
      canActivate: [AuthGuard],
      children: [
        {path: '', component: UserComponent},
        {path: 'form/:id', component: UserFormComponent}
      ]
    }
    ]
  },
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: '**', redirectTo: '/home' }
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
