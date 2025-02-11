import { Component, OnInit } from '@angular/core';
import { MessageService } from '../../../services/message.service';
import { UserMessagesResponse } from '../../../dtos/response/userMessagesResponse';
import { Router } from '@angular/router';
import { FriendService } from '../../../services/friend.service';
import { AuthService } from '../../../services/auth.service';
import { UserSearchResponse } from '../../../dtos/response/userSearchResponse';
import { FriendRequestResponse } from '../../../dtos/response/friendRequestResponse';

@Component({
  selector: 'app-social',
  templateUrl: './social.component.html',
  styleUrls: ['./social.component.css'],
})
export class SocialComponent implements OnInit {
  lastMessagesWithAllUsers: UserMessagesResponse[] = [];
  newMessagesWithAllUsers: UserMessagesResponse[] = [];
  searchTimeout: any = null;
  searchAllUsers: boolean = false;
  searchName: string = '';
  searchedUsers: UserSearchResponse[] = [];
  friendRequests: FriendRequestResponse[] = [];
  friendsMenuSelected: boolean = true;
  userSearchPage: number = 1;
  friendRequestPage: number = 1;

  constructor(
    private authService: AuthService,
    private messageService: MessageService,
    private friendService: FriendService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.messageService.getLastMessageWithAllUsers().subscribe((value) => {
      this.lastMessagesWithAllUsers = <UserMessagesResponse[]>value;
    });

    this.messageService.getNewMessageWithAllUsers().subscribe((value1) => {
      this.newMessagesWithAllUsers = <UserMessagesResponse[]>value1;
    });

    this.search();

    this.fetchFriendRequests();
  }

  fetchFriendRequests() {
    this.friendService
      .getFriendRequests(this.friendRequestPage)
      .subscribe((value) => {
        if (this.friendRequestPage == 1) {
          this.friendRequests = [];
        }

        for (let valueElement of value) {
          this.friendRequests.push(valueElement);
        }

        if (value.length) {
          ++this.friendRequestPage;
        }
      });
  }

  refreshFriendRequests() {
    this.friendRequestPage = 1;
    this.fetchFriendRequests();
  }

  getMessage(userMessage: UserMessagesResponse) {
    let message = userMessage.messages.at(0);

    return message ? message : null;
  }

  openDirectMessages(userId: number) {
    this.router.navigateByUrl(`social/dm/${userId}`);
  }

  respondToFriendRequest(requestId: number, accepted: boolean) {
    this.friendService
      .respondToFriendRequest(requestId, accepted)
      .subscribe((value) => {
        this.friendRequests = this.friendRequests.filter(
          (fr) => fr.id != value
        );
      });
  }

  search() {
    this.friendService
      .getUsersByName(
        this.authService.getUserId(),
        this.searchName.length ? this.searchName : '',
        this.userSearchPage,
        !this.searchAllUsers
      )
      .subscribe((value) => {
        if (this.userSearchPage == 1) {
          this.searchedUsers = [];
        }

        for (let valueElement of value) {
          this.searchedUsers.push(valueElement);
        }

        if (value.length) {
          ++this.userSearchPage;
        }
      });
  }

  refreshSearch(delay: boolean = true) {
    this.userSearchPage = 1;

    if (this.searchTimeout) {
      clearTimeout(this.searchTimeout);
      this.searchTimeout = null;
    }

    if (delay) {
      this.searchTimeout = setTimeout(() => {
        this.search();
      }, 700);
    } else {
      this.search();
    }
  }

  hasScrollReachedEnd(element: HTMLElement) {
    if (element.offsetHeight + element.scrollTop >= element.scrollHeight) {
      return true;
    }

    return false;
  }

  onScrollGetUsers(element: HTMLElement) {
    if (this.hasScrollReachedEnd(element)) {
      this.search();
    }
  }

  onScrollGetFriendRequests(element: HTMLElement) {
    if (this.hasScrollReachedEnd(element)) {
      this.fetchFriendRequests();
    }
  }

  changeMenu(selectFriendsMenu: boolean) {
    if (selectFriendsMenu) {
      this.friendsMenuSelected = true;

      if (!this.searchAllUsers) {
        this.refreshSearch(false);
      }
    } else {
      this.friendsMenuSelected = false;
      this.refreshFriendRequests();
    }
  }
}
