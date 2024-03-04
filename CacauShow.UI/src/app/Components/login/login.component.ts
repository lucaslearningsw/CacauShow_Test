import { Component, OnInit } from '@angular/core';
import { LoginUser } from '../models/loginUser.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
 
})
export class LoginComponent implements OnInit {

   loginObj: LoginUser
   loginFrom!: FormGroup;
  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) { 
    this.loginObj = new LoginUser();
  }


  ngOnInit(): void {
    this.loginFrom = this.fb.group({
      email: ['',Validators.required],
      password: ['',Validators.required]
    });

  }

  onLogin(){
      
    console.log(this.loginFrom.value)
    this.auth.Login(this.loginFrom.value).subscribe({
    next:(res)=>
    {
      alert(res.message);
      this.loginFrom.reset();
      this.router.navigate(['dashboard'])
    },
    error:(err)=>
    {
      alert(err?.error.message)
    }
    })
  }

}
