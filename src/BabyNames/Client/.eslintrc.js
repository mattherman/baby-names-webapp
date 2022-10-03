module.exports = {
	env: {
		browser: true,
		es2021: true,
		node: true,
	},
	extends: [
		'eslint:recommended',
		'plugin:react/recommended',
		'plugin:@typescript-eslint/recommended',
		'prettier',
	],
	parser: '@typescript-eslint/parser',
	parserOptions: {
		project: 'tsconfig.json',
		tsconfigRootDir: __dirname,
		sourceType: 'module',
	},
	plugins: ['react', '@typescript-eslint'],
	rules: {
		curly: 'error',
		'eol-last': 'warn',
		'guard-for-in': 'error',
		'max-len': ['error', { code: 180 }],
		'no-mixed-spaces-and-tabs': ['error', 'smart-tabs'],
		'react/prop-types': 'off',
		'@typescript-eslint/ban-types': [
			'error',
			{ types: { Function: false }, extendDefaults: true },
		],
		'@typescript-eslint/explicit-module-boundary-types': 'off',
		'@typescript-eslint/ban-types': 'off',
		'@typescript-eslint/consistent-type-assertions': 'error',
		'@typescript-eslint/naming-convention': [
			'error',
			{
				selector: 'parameter',
				format: ['camelCase'],
				leadingUnderscore: 'allow',
				trailingUnderscore: 'allow',
			},
			{
				selector: 'variable',
				format: ['camelCase'],
			},
			{
				selector: 'function',
				format: ['camelCase', 'PascalCase'],
			},
			{
				selector: ['typeLike', 'enumMember', 'class', 'interface'],
				format: ['PascalCase'],
			},
		],
		'@typescript-eslint/no-explicit-any': 'off',
		'@typescript-eslint/no-unused-vars': [
			'warn',
			{
				argsIgnorePattern: '^_',
				varsIgnorePattern: '^_',
			},
		],
	},
	settings: {
		react: {
			version: 'detect',
		},
	},
};

