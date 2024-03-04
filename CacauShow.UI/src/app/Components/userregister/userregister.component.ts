import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { userRegister } from '../models/userRegister';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-userregister',
  templateUrl: './userregister.component.html',
  styleUrls: ['./userregister.component.css']
})
export class UserregisterComponent implements OnInit {

  registerObj: userRegister
  registerFrom!: FormGroup;
  constructor(private fb: FormBuilder,private auth: AuthService , private router: Router) {
    this.registerObj = new userRegister();
   }

  ngOnInit(): void {

    this.registerFrom = this.fb.group({
      UserEmail: ['',Validators.required],
      password: ['',Validators.required],
      name: ['',Validators.required],
      city:['',Validators.required],
      
  })}

  onRegisterUser(){
    console.log(this.registerFrom.value)
    this.auth.RegisterUser(this.registerFrom.value).subscribe({
    next:(res)=>
    {
      this.registerFrom.reset();
      this.router.navigate(['login'])
    },
    error:(err)=>
    {
      alert(err?.error.message)
    }
    })
  }
}
