// See https://svelte.dev/docs/kit/types#app.d.ts
// for information about these interfaces
declare global {
	namespace App {
		interface Locals {
			user?: {
				id: string;
				email: string;
				firstName: string;
				lastName: string;
				roleId: string;
				roleName: string;
			};
			isAuthenticated: boolean;
		}
		// interface Error {}
		// interface PageData {}
		// interface PageState {}
		// interface Platform {}
	}
}

export {};
