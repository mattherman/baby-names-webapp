import { createSlice } from '@reduxjs/toolkit';
import { getToken, refreshToken } from './session.actions';

export interface ISessionState {
	token: string;
}

const initialState: ISessionState = {
	token: null,
};

const slice = createSlice({
	name: 'session',
	initialState,
	reducers: {},
	extraReducers: (builder) =>
		builder
			.addCase(getToken.fulfilled, (state, action) => {
				state.token = action.payload.token;
			})
			.addCase(refreshToken.fulfilled, (state, action) => {
				state.token = action.payload.token;
			}),
});

export default {
	...slice,
	actions: {
		...slice.actions,
		getToken,
		refreshToken,
	},
};

