import { Router } from '@angular/router';
import { NavItem } from './classes/nav-item';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Roles } from '../../enums/roles';

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
      { name: 'Reviews', url: '/home/reviews'},
    ];
    if(this.authService.getRole() == Roles.ADMIN){
      this.items.push(
        { name: 'ReviewsAdm', url: '/home/admin/reviews'},
        { name: 'UsersAdmin', url: '/home/admin/users'}
      )
    }
  }

  logout(){
    this.authService.logout();
    this.router.navigate(['/login']);
  }

}
