import { Component } from '@angular/core';
import { RouterService } from './../services/router.service';
import { Register } from './../register';
import { FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { HttpErrorResponse } from '@angular/common/http';
import { GoogleLoginProvider, AuthService } from 'angularx-social-login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  submitMessage: string="";
  showRegister:boolean = false;
  username = new FormControl('', [Validators.required]);
  password = new FormControl('', [Validators.required]);

  constructor(private authService: AuthenticationService, private routerService: RouterService, private OAuth: AuthService) {
    
  }

  ngOnInit() {
    this.authService.removeUserDetails();
  };



  registerUser() {
    this.routerService.routeToRegister();
  }


  loginSubmit() {
    const loginData = new Register(this.username.value, this.password.value);
    if (this.username.valid && this.password.valid) {
      this.authService.authenticateUser(loginData).subscribe(
        response => {
          // Set the token in localstorage
          this.authService.setBearerToken(response['token'],loginData.UserName);
          this.routerService.routeToDashboard();
        },
        err => {
          if(err.status==404)
          {
            this.showRegister=true;
            this.clear();
          }
          else{
            if(HttpErrorResponse)
            {
              this.submitMessage = "Server Not Available, Try again later";
            }
            else
            {
              this.submitMessage = (err.status === 403) || (err.error) ? err.error.message : err.message ;
            }
          }
        }
      );
    }
  }

  clear(){
    this.username.reset();
    this.password.reset();
  }

  registeruser(){
    if( this.showRegister==true){
    this.routerService.routeToRegister();}
  }
  
  getUserNameErrorMessage() {
    // Required error message for username
    return this.username.hasError('required') ? 'You must enter a Username' : '';
  }
  
  getUserNotFoundError() {
    // Required error message for username
    return 'UserName does not exist, Please register!';
  }

  getPasswordErrorMessage() {
    // Required error message for password
    return this.password.hasError('required') ? 'You must enter a password' : '';
  }

  public signInWithGoogle(socialProvider: string) {    
    let socialPlatformProvider;    
    if (socialProvider === 'google') {    
      socialPlatformProvider = GoogleLoginProvider.PROVIDER_ID;    
    }    
    
    this.OAuth.signIn(socialPlatformProvider).then(socialusers => {    
      this.authService.setSocialBearerToken(socialusers.email);
      this.routerService.routeToNote();
      
    });    
  }    
}
