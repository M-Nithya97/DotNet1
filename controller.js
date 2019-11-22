//(function () {
//    'use strict';

//    angular
//        .module('app')
//        .controller('controller', controller);

//    controller.$inject = ['$location'];

//    function controller($location) {
//        /* jshint validthis:true */
//        var vm = this;
//        vm.title = 'controller';

//        activate();

//        function activate() { }
//    }
//})();
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';
@Injectable({
    providedIn: 'root'
})
export class SignalRService {
    private message$: Subject<message>;
    private connection: signalR.HubConnection;

    constructor() {
        this.message$ = new Subject<message>();
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(environment.hubUrl)
            .build();
        this.connect();
    }
    private connect() {
        this.connection.start().catch(err => console.log(err));
        this.connection.on('SendMessage', (message) => {
            this.message$.next(message);
        });
    }
    public getMessage(): Observable<message> {
        return this.message$.asObservable();
    }
    public disconnect() {
        this.connection.stop();
    }
}

