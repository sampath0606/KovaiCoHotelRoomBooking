import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

import { RouterService } from './../services/router.service';
import { AuthenticationService } from '../services/authentication.service';
import { AuthService } from 'angularx-social-login';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isAddView;

  constructor(private routerService: RouterService, private location: Location, private authservice: AuthenticationService,
    private OAuth :AuthService) {}

  ngOnInit() {
    // this.isAddView = (location.pathname.includes('/view/noteview')) ? true :
    //                   (location.pathname.includes('/view/listview')) ? false : true;
    this.isAddView=true;
  }

  switchToListView() {
    this.isAddView = false;
    this.routerService.routeToListView();
  }
  switchToNoteView() {
    this.isAddView = true;
    this.routerService.routeToDashboard();
  }

  logout() {
    this.authservice.removeUserDetails();
    var isSocial = localStorage.getItem("isSocaialLogin");
    if (isSocial) {
      this.OAuth.signOut().then(data => {    
         this.routerService.routeToLogin();  
      });
    }else{
    this.routerService.routeToLogin(); 
  }
    
    }

  isUserPresent():boolean{
   if(localStorage.getItem('username'))
    {
      return true;
      
    }
  }
}
