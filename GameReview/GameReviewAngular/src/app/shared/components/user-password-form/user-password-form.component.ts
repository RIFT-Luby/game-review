import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { User } from '../../entities/user';
import { UserAdminService } from '../../services/user-admin.service';

@Component({
  selector: 'app-user-password-form',
  templateUrl: './user-password-form.component.html',
  styleUrls: ['./user-password-form.component.scss']
})
export class UserPasswordFormComponent implements OnInit {

  form!: FormGroup;
  id!: any;

  constructor(
    private formBuilder: FormBuilder,
    private userAdminService: UserAdminService,
    private router: Router,
    protected http: HttpClient,
    private snackBar: MatSnackBar,
    private activatedroute:ActivatedRoute
  ) {
    this.form = this.formBuilder.group({
      password: [null, [Validators.required]],
      confirmPassword: [null, [Validators.required]],
    })
  }

  async ngOnInit(): Promise<void> {
    this.activatedroute.paramMap.subscribe(params => {
      this.id = params.get('id');
    });
  }

  async savePasswordAsync(): Promise<void> {
    try {
      if(this.isFormValid()) {
        const data = this.form.value as User;
        await lastValueFrom(this.userAdminService.updatePassword(data, this.id));
        this.snackBar.open('Password saved!', undefined, { duration: 3000 });
        //if(this.isAdmin) {
          this.router.navigate(['/home/admin/users/']);
        //}
        //else {
          //this.router.navigate(['/dashboard/agenda/']);
        //}
      }
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

}
