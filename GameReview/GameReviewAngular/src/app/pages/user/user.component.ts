import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { ConfirmModalService } from 'src/app/shared/components/confirm-modal/services/confirm-modal.service';
import { User } from 'src/app/shared/entities/user';
import { AuthService } from 'src/app/shared/services/auth.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  user!: User;

  constructor(
    private userService: UserService,
    private router: Router,
    private confirmModalService: ConfirmModalService,
    private authService: AuthService
  ) { }

  async ngOnInit(): Promise<void> {
    await this.getDataAsync();
  }

  async getDataAsync(): Promise<void> {
    this.user = await lastValueFrom(this.userService.getUser());
  }

  onEdit() {
    this.router.navigate(['/home/user/form/', this.user.id]);
  }

  async deleteUserAsync(): Promise<void> {
    this.confirmModalService.open();
    this.confirmModalService.closed.subscribe(async (result) => {
      if(result) {
        await lastValueFrom(this.userService.deleteUser());
        this.logout();
      }
    });
  }

  logout(){
    this.authService.logout();
    this.router.navigate(['/login']);
  }

}
