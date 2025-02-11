import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { UserSearchResponse } from '../dtos/response/userSearchResponse';
import { FriendRequestResponse } from '../dtos/response/friendRequestResponse';

@Injectable({
  providedIn: 'root',
})
export class FriendService {
  URL = 'https://localhost:7025/api';

  constructor(private http: HttpClient, private auth: AuthService) {}

  isFriend(checkFriendId: number) {
    return this.http.get<boolean | null>(
      `${
        this.URL
      }/Friend/IsFriend?askerId=${this.auth.getUserId()}&askedId=${checkFriendId}`
    );
  }

  sendFriendRequest(sendToId: number) {
    return this.http.post(
      `${
        this.URL
      }/Friend/SendFriendRequest?senderId=${this.auth.getUserId()}&receiverId=${sendToId}`,
      null
    );
  }

  getUsersByName(
    searcherId: number,
    name: string,
    page: number,
    searchFriendsOnly: boolean = true
  ) {
    return this.http.get<UserSearchResponse[]>(
      this.URL +
        `/Friend/SearchUsersByName?searcherId=${searcherId}&page=${page}&name=` +
        (name ? name : '') +
        `&searchFriendsOnly=${searchFriendsOnly}`
    );
  }

  getFriendRequests(page: number) {
    return this.http.get<FriendRequestResponse[]>(
      `${
        this.URL
      }/Friend/GetFriendRequests?toUserId=${this.auth.getUserId()}&page=${page}`
    );
  }

  respondToFriendRequest(requestId: number, accepted: boolean) {
    return this.http.post<number>(
      `${this.URL}/Friend/RespondToFriendRequest?requestId=${requestId}&accepted=${accepted}`,
      null
    );
  }

  removeFriend(toRemoveId: number) {
    return this.http.post<number>(
      `${
        this.URL
      }/Friend/RemoveFriend?senderId=${this.auth.getUserId()}&toRemoveId=${toRemoveId}`,
      null
    );
  }
}
