import type { Branch } from '../types/branch'

const BRANCHES_URL = '/api/branches'

export async function getBranches(): Promise<Branch[]> {
  const response = await fetch(BRANCHES_URL)

  if (!response.ok) {
    throw new Error(`Request failed with status ${response.status}. Is the backend running?`)
  }

  return response.json() as Promise<Branch[]>
}

export async function createBranch(branchName: string): Promise<Branch> {
  const response = await fetch(`${BRANCHES_URL}/create`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ branchName }),
  })

  if (!response.ok) {
    throw new Error(`Could not create the branch (status ${response.status}).`)
  }

  return response.json() as Promise<Branch>
}
