import { createAsyncThunk } from '@reduxjs/toolkit';
import * as tokenApi from '~/api/token';

export const getToken = createAsyncThunk('getToken', async () => {
	return await tokenApi.getToken();
});

export const refreshToken = createAsyncThunk('refreshToken', async () => {
	return await tokenApi.refreshToken();
});

