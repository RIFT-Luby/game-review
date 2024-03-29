import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from './../services/auth.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router ,RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { Roles } from '../enums/roles';

@Injectable({
  providedIn: 'root'
})
export class AdminAuth implements CanActivate {

  constructor(
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar){}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    const role = this.authService.getRole();

    if(role != Roles.ADMIN){
      this.snackBar.open("The user not is not ADMIN", undefined, {duration: 5000});
      this.router.navigate(['/home']);
      return false;
    }

    return true;

  }

}
