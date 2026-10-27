import { useEffect, useState } from 'react'
import { createBranch as createBranchRequest, getBranches } from '../api/branches'
import type { Branch } from '../types/branch'

export function useBranches(){
  const [branches, setBranches] = useState<Branch[]>([])
  const [error, setError] = useState<string | null>(null)
  const [isLoading, setIsLoading] = useState(true)  

    async function refreshBranches(): Promise<void> {
        try {
            setIsLoading(true)
            setError(null)

            const loadedBranches = await getBranches()

            setBranches(loadedBranches)
        } catch (requestError) {
            setError(requestError instanceof Error ? requestError.message : "Something went wrong.")
        } finally {
            setIsLoading(false)
        }
    }

    async function addBranch(branchName: string): Promise<boolean> {
        try {
            setError(null)

            const newBranch = await createBranchRequest(branchName)
            
            setBranches((currentBranches) => [newBranch, ...currentBranches])

            return true
        } catch (requestError) {
            setError(requestError instanceof Error ? requestError.message : 'Could not create the branch.')

            return false
        }
    }

    useEffect(() => {
        let ignoreResult = false

        void getBranches()
        .then((data) => {
            if (!ignoreResult) setBranches(data)
        })
        .catch((requestError: unknown) => {
            if (!ignoreResult) {
            setError(requestError instanceof Error ? requestError.message : 'Something went wrong.')
            }
        })
        .finally(() => {
            if (!ignoreResult) setIsLoading(false)
        })

        return () => {
        ignoreResult = true
        }
    }, [])

    return { branches, isLoading, error ,refreshBranches, addBranch}
}