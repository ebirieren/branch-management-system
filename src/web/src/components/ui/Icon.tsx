import type { ReactNode } from 'react'
import '../../app/App.css'

export type IconName =
  | 'grid'
  | 'branch'
  | 'box'
  | 'receipt'
  | 'search'
  | 'plus'
  | 'refresh'
  | 'warehouse'
  | 'layers'
  | 'value'
  | 'close'

export function Icon({ name, size = 20 }: { name: IconName; size?: number }) {
  const paths: Record<IconName, ReactNode> = {
    grid: <><rect x="3" y="3" width="7" height="7" rx="1"/><rect x="14" y="3" width="7" height="7" rx="1"/><rect x="3" y="14" width="7" height="7" rx="1"/><rect x="14" y="14" width="7" height="7" rx="1"/></>,
    branch: <><circle cx="6" cy="5" r="2"/><circle cx="18" cy="5" r="2"/><circle cx="12" cy="19" r="2"/><path d="M6 7v3c0 2 1.5 3 3 3h6c1.5 0 3-1 3-3V7M12 13v4"/></>,
    box: <><path d="m4 7 8-4 8 4-8 4-8-4Z"/><path d="m4 7 8 4 8-4v10l-8 4-8-4V7Z"/><path d="M12 11v10"/></>,
    receipt: <><path d="M6 3h12v18l-3-2-3 2-3-2-3 2V3Z"/><path d="M9 8h6M9 12h6"/></>,
    search: <><circle cx="11" cy="11" r="7"/><path d="m20 20-4-4"/></>,
    plus: <path d="M12 5v14M5 12h14"/>,
    refresh: <><path d="M20 11a8 8 0 0 0-14-5l-2 2"/><path d="M4 4v4h4M4 13a8 8 0 0 0 14 5l2-2"/><path d="M20 20v-4h-4"/></>,
    warehouse: <><path d="M3 9 12 3l9 6v12H3V9Z"/><path d="M7 21v-8h10v8M9 16h6"/></>,
    layers: <><path d="m12 3 9 5-9 5-9-5 9-5Z"/><path d="m3 12 9 5 9-5M3 16l9 5 9-5"/></>,
    value: <><circle cx="12" cy="12" r="9"/><path d="M16 8.5c-.8-.7-2-1-3.2-1-1.6 0-2.8.8-2.8 2s1 1.8 3 2.3c2 .5 3 1.1 3 2.5s-1.3 2.2-3 2.2c-1.4 0-2.8-.5-3.7-1.3M12.7 5.5v2M12.7 16.5v2"/></>,
    close: <path d="m6 6 12 12M18 6 6 18"/>,
  }

  return (
    <svg width={size} height={size} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.8" strokeLinecap="round" strokeLinejoin="round" aria-hidden="true">
      {paths[name]}
    </svg>
  )
}