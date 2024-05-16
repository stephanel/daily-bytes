import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { User } from '../models/user.model';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-account',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-account.component.html',
  styleUrl: './user-account.component.css',
})
export class UserAccountComponent implements OnInit {
  user?: User;

  constructor(
    private route: ActivatedRoute, 
    private userService: UserService) 
    { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      let userId = Number(params.get('id'));
      this.userService.get(userId).subscribe(user => this.user = user);
    });
  }
}
