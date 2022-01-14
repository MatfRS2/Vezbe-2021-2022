import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthenticationFacadeService } from '../../domain/application-services/authentication-facade.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css'],
})
export class LogoutComponent implements OnInit {
  public logoutSuccess$: Observable<boolean>;

  constructor(private authenticationService: AuthenticationFacadeService) {
    this.logoutSuccess$ = this.authenticationService.logout();
  }

  ngOnInit(): void {}
}
