import {AuthService} from "@signature/webfrontauth";
import {appSettings} from "@/modules/config/appSettings";
import axios from 'axios';

let instance: AuthService;
export async function initializeAuthService(): Promise<AuthService> {
    instance = await AuthService.createAsync( { identityEndPoint: appSettings.AuthService }, axios );
    return instance;
}
export const getAuthService:() =>AuthService = () => instance;
export * from "@signature/webfrontauth";