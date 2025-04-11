import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService } from '../../services/user.service';
import { ActionBtnComponent } from '../action-btn/action-btn.component';

@Component({
  selector: 'app-user-table',
  standalone: true,
  imports: [
    CommonModule, 
    ActionBtnComponent
  ],
  templateUrl: './user-table.component.html',
  styleUrls: ['./user-table.component.css']
})
export class UserTableComponent implements OnInit {
  users: any[] = [];

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService.getUsers().subscribe({
      next: (data: any) => {
        this.users = data;
      },
      error: (err: any) => {
        console.error('Error fetching users:', err);
      }
    });
  }
}
