import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { lastValueFrom, Observable } from 'rxjs';
import { Enumeration } from '../../entities/enumeration';
import { User } from '../../entities/user';
import { ApiBaseService } from '../../services/api-base.service';
import { UserAdminService } from '../../services/user-admin.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.scss']
})
export class UserFormComponent implements OnInit {
  form!: FormGroup;
  roles!: Enumeration[];
  id!: any;
  changePassword = false;
  isEditMode!: boolean;
  service!: ApiBaseService<any>;
  user!: User;
  isAdmin = false;

  constructor(
    private formBuilder: FormBuilder,
    private userAdminService: UserAdminService,
    private userService: UserService,
    private router: Router,
    protected http: HttpClient,
    private snackBar: MatSnackBar,
    private activatedroute:ActivatedRoute
  ) {
    this.form = this.formBuilder.group({
      id: [null],
      name: [null, [Validators.required]],
      userName: [null, [Validators.required]],
      email: [null, [Validators.required, Validators.email]],
      userRoleId: [null, [Validators.required]],
      userRole: [null],
    })
    //TODO criar um endpoint getUserRoles na api
    this.roles = [{'id': 1, 'name': 'Admin'}, {'id': 2, 'name': 'Common'}];
    this.verifyIfIsEditMode();

  }

  async getUserAsync(): Promise<void> {
    this.user = await lastValueFrom(this.userService.getUser());
  }

  async IsAdminAsync(): Promise<void> {
    if(this.user.userRoleId == 1) {
      this.service = this.userAdminService;
      this.isAdmin = true;
    }else {
      this.service = this.userService;
      this.isAdmin = false;
    }
  }

  addPasswordForm(): void {
    this.form = this.formBuilder.group({
      ...this.form.controls,
      password: [null, Validators.required],
      confirmPassword: [null, Validators.required],
   });
  }

  async ngOnInit(): Promise<void> {
    this.activatedroute.paramMap.subscribe(params => {
      this.id = params.get('id');
    });

    await this.getUserAsync();
    await this.IsAdminAsync();
    await this.fillForm();
    this.verifyIfIsEditMode();
  }

  async saveUserAdminAsync(): Promise<void> {
    try {
      if(this.isFormValid()) {
        const data = this.form.value as User;
        data.id ?
          await lastValueFrom(this.userAdminService.update(data, data.id)) :
          await lastValueFrom(this.userAdminService.create(data))
        this.snackBar.open('User saved!', undefined, { duration: 3000 });
        this.router.navigate(['/home/admin/users/']);
      }
    }
    catch({error}) {
      console.log(error);
    }
  }

  async saveCommonUserAsync(): Promise<void> {
    try {
      if(this.isFormValid()) {
        const data = this.form.value as User;
        await lastValueFrom(this.userService.updateUser(data))
        this.snackBar.open('User saved!', undefined, { duration: 3000 });
        this.router.navigate(['/home/user/']);
      }
    }
    catch({error}) {
      console.log(error);
    }
  }

  verifyIfIsEditMode(): void {
    if(this.id != 0) {
      this.isEditMode = true;
    }else {
      this.isEditMode = false;
      this.addPasswordForm();
    }
  }

  async fillForm(): Promise<void> {
    try {
      this.form.get('id')?.setValue(this.user.id);
      this.form.get('name')?.setValue(this.user.name);
      this.form.get('userName')?.setValue(this.user.userName);
      this.form.get('email')?.setValue(this.user.email);
      this.form.get('userRoleId')?.setValue(this.user.userRoleId);
    }
    catch({error}) {
      console.log(error);
    }
  }

  isFormValid(): boolean {
    const isValid = this.form.valid;
    if (!isValid) {
      this.form.markAllAsTouched();
      this.snackBar.open('There are invalid fields in the form!', undefined, { duration: 3000 });
    }else {
      //implementar erro
    }
    return isValid;
  }

  async saveAsync(): Promise<void> {
    console.log(this.isAdmin)
    this.isAdmin ? await this.saveUserAdminAsync() : await this.saveCommonUserAsync();
  }
}
