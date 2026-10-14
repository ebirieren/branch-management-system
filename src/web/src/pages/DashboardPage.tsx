import { useMemo, useState } from 'react'
import { DashboardStats } from '../components/dashboard/DashboardStats'
import { Icon } from '../components/ui/Icon'
import { useBranches } from '../features/branches/hooks/useBranches'
import { DashboardLayout } from '../layouts/DashboardLayout'
import { CreateBranchDialog } from '../features/branches/components/CreateBranchDialog'
import { BranchSection } from '../features/branches/components/BranchSection'

export function DashboardPage(){
    const { branches, isLoading, error, refreshBranches, addBranch} = useBranches()
  const [search, setSearch] = useState('')
  const [isDialogOpen, setIsDialogOpen] = useState(false)

  const filteredBranches = useMemo(() => {
    const term = search.trim().toLowerCase()
    if (!term) return branches
    return branches.filter((branch) => branch.branchName.toLowerCase().includes(term))
  }, [branches, search])

  return (
    <DashboardLayout branchCount={branches.length} search={search} onSearchChange={setSearch}>

        <div className="page" id="dashboard">
          <section className="page-heading">
            <div>
              <p className="eyebrow">OVERVIEW</p>
              <h1>Good morning, Developer.</h1>
              <p>Here is what is happening across your branches today.</p>
            </div>
            <button className="primary-button" type="button" onClick={() => setIsDialogOpen(true)}>
              <Icon name="plus" size={18} /> Add branch
            </button>
          </section>

          <DashboardStats branches={branches} />

          <BranchSection branches={filteredBranches} isLoading={isLoading} error={error} search={search} onRefresh={refreshBranches} onAddBranch={() => setIsDialogOpen(true)} />
        </div>

      {isDialogOpen && (
        <CreateBranchDialog onClose={() => setIsDialogOpen(false)} onCreate={addBranch} />
      )}
    </DashboardLayout>
  )

}