# React + TypeScript learning guide

This first UI is a **Branches dashboard**. It reads branches from the C# API and can create a new branch.

## 1. The files and their jobs

- `src/main.tsx` starts React and renders the application.
- `src/App.tsx` is the page component. It owns page state and connects the smaller pieces.
- `src/types/branch.ts` describes the shape of the JSON returned by the backend.
- `src/api/branches.ts` contains the HTTP requests made with `fetch`.
- `src/components/StatCard.tsx` and `BranchCard.tsx` are reusable UI components.
- `src/App.css` controls the visual design and responsive layout.
- `vite.config.ts` forwards `/api` requests to the backend during development.

## 2. TypeScript: describing data

```ts
export type Branch = {
  id: number
  branchName: string
  products: BranchProduct[]
}
```

A TypeScript `type` is a description. It does not create data. It tells the editor and compiler what a branch must look like, so mistakes can be found before the browser runs the code.

## 3. React components and props

```tsx
<StatCard label="Total branches" value="4" />
```

A component is a function that returns UI. Props are the input values passed to that function. Reusing a component prevents us from copying the same markup many times.

## 4. State

```ts
const [search, setSearch] = useState('')
```

State is information that can change while the page is open. `search` is the current value. `setSearch` changes it. React renders the component again after state changes.

## 5. Effects and API requests

```ts
useEffect(() => {
  void getBranches().then((data) => setBranches(data))
}, [])
```

An effect runs work outside rendering. The empty array means “run once when this component first appears.” `getBranches` calls the API and the completed request stores the result in state. The production code also handles errors and prevents an old request from updating an unmounted component.

## 6. Derived values

`useMemo` calculates filtered branches and dashboard totals from existing state. These values do not need their own state because React can calculate them from `branches` and `search`.

## 7. Run both applications

Start the backend in one terminal:

```bash
dotnet run --project src/app/API
```

Start the UI in another terminal:

```bash
cd src/web
npm run dev
```

Open the URL printed by Vite, normally `http://localhost:5173`.

## 8. Small exercises

1. Change the greeting in `App.tsx`.
2. Add a fifth `StatCard` to understand props.
3. Add a `supplierName` field to a product type.
4. Add a branch details screen after you understand the current page.

Do one exercise at a time. Run `npm run build` after each change; TypeScript will explain many mistakes before runtime.
