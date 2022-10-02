import { createSlice } from '@reduxjs/toolkit';
import { getRemainingBabyNames, submitVote } from './names.actions';
import { IBabyName } from '~/models';

export interface IBabyNamesState {
	isLoading: boolean;
	hasError: boolean;
	remainingNames: IBabyName[];
	currentName: IBabyName;
}

const initialState: IBabyNamesState = {
	isLoading: false,
	hasError: false,
	remainingNames: [],
	currentName: null,
};

const slice = createSlice({
	name: 'names',
	initialState,
	reducers: {},
	extraReducers: (builder) =>
		builder
			.addCase(getRemainingBabyNames.pending, (state) => {
				state.isLoading = true;
				state.hasError = false;
				state.remainingNames = [];
				state.currentName = null;
			})
			.addCase(getRemainingBabyNames.fulfilled, (state, action) => {
				state.isLoading = false;
				const names = action.payload;
				state.currentName = names[0];
				state.remainingNames = names.slice(1);
			})
			.addCase(getRemainingBabyNames.rejected, (state) => {
				state.isLoading = false;
				state.hasError = true;
			})
			.addCase(submitVote.pending, (state) => {
				state.isLoading = true;
				state.hasError = false;
			})
			.addCase(submitVote.fulfilled, (state) => {
				state.isLoading = false;
				if (state.remainingNames.length > 0) {
					state.currentName = state.remainingNames[0];
					state.remainingNames = state.remainingNames.slice(1);
				} else {
					state.currentName = null;
				}
			})
			.addCase(submitVote.rejected, (state) => {
				state.isLoading = false;
				state.hasError = true;
			})
});

export default {
	...slice,
	actions: {
		...slice.actions,
		getRemainingBabyNames,
		submitVote,
	},
};
