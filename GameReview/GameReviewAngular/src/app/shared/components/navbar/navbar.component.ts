import { Roles } from './../../../../../src - Copia/app/shared/enums/roles';
import { Router } from '@angular/router';
import { AuthService } from './../../../../../src - Copia/app/shared/services/auth.service';
import { NavItem } from './classes/nav-item';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  items: NavItem[] = [];

  constructor(
    private authService: AuthService,
    private router: Router) { }

  ngOnInit(): void{
    this.setItems();
  }

  setItems(): void{
    this.items = [
      { name: 'Home', url: '/home'},
      { name: 'Reviews', url: '/home/reviews'}
    ];
    if(this.authService.getRole() == Roles.ADMIN){
      this.items.push(
      )
    }
  }

  logout(){
    this.authService.logout();
    this.router.navigate(['/login']);
  }

}
