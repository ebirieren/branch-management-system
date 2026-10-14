import { useState } from "react"
import type { FormEvent } from "react"
import { Icon } from '../../../components/ui/Icon'

type CreateBranchDialogProps = {
    onClose: () => void
    onCreate: (branchName: string) => Promise<boolean>
}

export function CreateBranchDialog({onClose, onCreate,}: CreateBranchDialogProps) {
    const [branchName, setBranchName] = useState('')
    const [isSaving, setIsSaving] = useState(false)

    async function handleSubmit(event: FormEvent<HTMLFormElement>,){
        event.preventDefault()

        const cleanName = branchName.trim()

        if(!cleanName){
            return 
        }

        try {
            setIsSaving(true)

            const wasCreated = await onCreate(cleanName)

            if (wasCreated){
                onClose()
            }
        } finally{
            setIsSaving(false)
        }
    }

        return (
            <div className="dialog-backdrop" role="presentation" onMouseDown={onClose}>
            <section className="dialog" role="dialog" aria-modal="true" aria-labelledby="dialog-title" onMouseDown={(event) => event.stopPropagation()}>
                <button className="dialog-close" type="button" onClick={onClose} aria-label="Close dialog"><Icon name="close" /></button>
                <span className="dialog-icon"><Icon name="warehouse" /></span>
                <p className="eyebrow">NEW LOCATION</p>
                <h2 id="dialog-title">Add a branch</h2>
                <p>Give the new branch a clear, recognizable name.</p>
                <form onSubmit={handleSubmit}>
                    <label htmlFor="branch-name">Branch name</label>
                    <input id="branch-name" autoFocus placeholder="Example: Kadıköy Branch" value={branchName} onChange={(event) => setBranchName(event.target.value)} />
                    <div className="dialog-actions">
                        <button className="secondary-button" type="button" onClick={onClose}>Cancel</button>
                        <button className="primary-button" type="submit" disabled={isSaving || !branchName.trim()}>{isSaving ? 'Creating...' : 'Create branch'}</button>
                    </div>
                </form>
            </section>
            </div>
        )
}
