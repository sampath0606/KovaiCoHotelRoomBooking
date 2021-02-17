import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Injectable()
export class RouterService {

  constructor(private router: Router, private location: Location) { }
  routeToDashboard() {
    // route to dashboard
    this.router.navigate(['dashboard']);
  }
  routeToLogin() {
    // route to login
    this.router.navigate(['login']);
  }

  routeToNote(){
    this.router.navigate(['dashboard/addnote']);

  }

  routeToRegister() {
    // route to login
    this.router.navigate(['register']);
  }

  routeBack() {
    // route back from edit to Read view
    this.location.back();
  }

  routeToListView() {
    // route to list view
    this.router.navigate(['dashboard/view/listview']);
  }

  

  
}
