import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { Message } from '../../../../models/message';

@Component({
  selector: 'app-message-card',
  templateUrl: './message-card.component.html',
  styleUrls: ['./message-card.component.css'],
})
export class MessageCardComponent implements OnInit {
  @Input() userId: number = 0;
  @Input() username: string = '';
  @Input() message: Message | null = null;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  isMessageFromCurrentUser() {
    return this.authService.getUserId() == this.message?.senderId;
  }
}
