import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { Message } from '../../../../models/message';
import { MessageService } from '../../../services/message.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-direct-message',
  templateUrl: './direct-message.component.html',
  styleUrls: ['./direct-message.component.css'],
})
export class DirectMessageComponent implements OnInit, OnDestroy {
  messages: Message[] = [];
  messageToSend: string = '';
  receiverId: number | null = null;
  isFetchingMessagesScrollTop: boolean = false;
  isFetchingMessagesInterval: boolean = false;
  newMessagesInterval: any = null;
  recieverUsername: string = '';

  constructor(
    private messageService: MessageService,
    private route: ActivatedRoute,
    private authService: AuthService
  ) {}

  ngOnInit() {
    let recId: string | null = this.route.snapshot.paramMap.get('id');
    if (recId != null) this.receiverId = +recId;

    if (this.receiverId != null) {
      this.messageService
        .getLatestMessages(this.receiverId)
        .subscribe((data) => {
          this.messages = <Message[]>data;

          this.newMessagesInterval = setInterval(() => {
            this.pingMessages();
          }, 1000);
        });
    }

    this.authService
      .getUser(parseInt(recId!))
      .subscribe((x) => (this.recieverUsername = x.username));
  }

  ngOnDestroy(): void {
    if (this.newMessagesInterval) clearInterval(this.newMessagesInterval);
  }

  isMessageFromSender(message: Message) {
    return this.messageService.isMessageFromSender(message);
  }

  btnSend() {
    if (this.receiverId != null) {
      this.messageService
        .sendMessage(this.receiverId, this.messageToSend)
        .subscribe((data) => {
          this.messageToSend = '';
        });
    }
  }

  onScrollMessagesDisplay(element: HTMLElement) {
    if (
      this.receiverId != null &&
      this.messages.length &&
      !this.isFetchingMessagesScrollTop &&
      element.offsetHeight - element.scrollTop >= element.scrollHeight
    ) {
      this.isFetchingMessagesScrollTop = true;

      this.messageService
        .getMessages(
          this.receiverId,
          this.messages[this.messages.length - 1].id
        )
        .subscribe((data) => {
          this.isFetchingMessagesScrollTop = false;
          this.messages = this.messages.concat(<Message[]>data);
        });
    }
  }

  pingMessages() {
    if (this.receiverId && !this.isFetchingMessagesInterval) {
      this.isFetchingMessagesInterval = true;

      this.messageService
        .getNewMessages(
          this.receiverId,
          this.messages && this.messages.length ? this.messages[0].id : null
        )
        .subscribe((data2) => {
          if (data2 && (<Message[]>data2).length)
            this.messages = (<Message[]>data2).concat(this.messages);
          this.isFetchingMessagesInterval = false;
        });
    }
  }
}
