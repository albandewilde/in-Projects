import AuthService from '../services/AuthService'
import { AssertThatUserIsInGroup } from '../api/groupsApi'

/**
 * Filter for routes requiring an authenticated user
 * @param {*} to 
 * @param {*} from 
 * @param {*} next 
 */

export default async function requireAuth(to, from, next) {
    if (!AuthService.isConnected) {
        next({
            path: '/login',
            query: { redirect: to.fullPath }
        });

        return;
    }

    var requiredProviders = to.meta.requiredProviders;

    if (requiredProviders && !AuthService.isBoundToProvider(requiredProviders)) {
        next({
            path: '/'
        });

        return;
    }

    next();
}