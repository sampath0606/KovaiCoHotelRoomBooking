import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { RouterService } from '../services/router.service';
import { Register } from '../register';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  errorMessage:string;
  username = new FormControl('', [Validators.required]);
  password = new FormControl('', [Validators.required]);
  constructor(private authservice: AuthenticationService, private routerservice: RouterService) { }

  ngOnInit() {
    this.authservice.removeUserDetails();
  };


  clear(){
    this.username.reset();
    this.password.reset();
  }

  loginpage()
  {
    this.routerservice.routeToLogin();
  }

  registerUser(){
    const userDetail = new Register(this.username.value, this.password.value);
    if(this.username.valid && this.password.valid){
      this.authservice.registeruser(userDetail).subscribe(
      response=>{
        alert("User Added");
        this.routerservice.routeToLogin();
      },
      err=>{
        if(err.status==499)
        {
          alert("User Already Available, Routing to Login Page");
          this.routerservice.routeToLogin();
        }
        else{
        this.errorMessage = (err.error) ? err.error : err.message ;
        alert(this.errorMessage);
      }}
      );
    }
    
  }

  getUserNameErrorMessage() {
    // Required error message for username
    return this.username.hasError('required') ? 'You must enter a Username' : '';
  }
  
 getPasswordErrorMessage() {
    // Required error message for password
    return this.password.hasError('required') ? 'You must enter a password' : 'Invalid Entry';
  }
}
