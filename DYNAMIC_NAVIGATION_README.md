# Dynamic Navigation System

This document describes the dynamic navigation system implemented for the Alumni Network application, which allows navigation to be managed from the database and displayed based on user roles.

## Overview

The navigation system consists of:
- **Backend**: .NET Core API with navigation entities, repositories, and controllers
- **Frontend**: SvelteKit with dynamic navigation components and stores
- **Database**: Navigation items stored with role-based permissions

## Backend Architecture

### Entities

#### NavigationGroup
- `Id`: Unique identifier
- `Name`: Group name (e.g., "Main", "Administration", "Content")
- `Description`: Optional description
- `Order`: Display order
- `IsActive`: Whether the group is active
- `NavigationItems`: Collection of navigation items

#### NavigationItem
- `Id`: Unique identifier
- `Label`: Display text
- `Icon`: Icon name (maps to Lucide icons)
- `Url`: Navigation URL
- `Order`: Display order within group
- `ParentId`: Parent navigation item (for hierarchical navigation)
- `GroupId`: Associated navigation group
- `IsActive`: Whether the item is active
- `Children`: Collection of child navigation items

#### RoleNavigation
- `RoleId`: Role identifier
- `NavigationItemId`: Navigation item identifier
- Links roles to navigation items for permission control

### API Endpoints

#### GET /api/navigation/user
Returns navigation items for the authenticated user based on their role.

**Response:**
```json
{
  "success": true,
  "data": {
    "groups": [
      {
        "id": "guid",
        "name": "Main",
        "description": "Main navigation items",
        "order": 1,
        "isActive": true,
        "navigationItems": [...]
      }
    ],
    "flatItems": [...]
  }
}
```

#### GET /api/navigation/role/{roleId}
Returns navigation items for a specific role (admin only).

### Use Cases

- `GetUserNavigationQuery`: Retrieves navigation for authenticated user
- Handles role-based filtering and hierarchical structure

## Frontend Architecture

### Components

#### Navigation.svelte
Reusable navigation component that:
- Loads navigation data from the store
- Renders navigation items with icons
- Handles active states
- Supports different types (main, admin, mobile)
- Supports grouped display

**Props:**
- `type`: 'main' | 'admin' | 'mobile'
- `showGroups`: boolean (show navigation groups)
- `className`: CSS classes

### Stores

#### navigationStore
Manages navigation state:
- `loadUserNavigation()`: Load navigation for current user
- `loadNavigationByRole(roleId)`: Load navigation for specific role
- `clear()`: Clear navigation data
- Helper methods for filtering navigation

### API Integration

#### api.ts
Added navigation endpoints:
- `getUserNavigation()`: Get user's navigation
- `getNavigationByRole(roleId)`: Get role-based navigation

## Database Schema

### Navigation Groups
```sql
CREATE TABLE navigation_groups (
    id UUID PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    order_index INTEGER NOT NULL,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

### Navigation Items
```sql
CREATE TABLE navigation_items (
    id UUID PRIMARY KEY,
    label VARCHAR(255) NOT NULL,
    icon VARCHAR(100),
    url VARCHAR(500),
    order_index INTEGER NOT NULL,
    parent_id UUID REFERENCES navigation_items(id),
    group_id UUID REFERENCES navigation_groups(id),
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

### Role Navigation
```sql
CREATE TABLE role_navigation (
    id UUID PRIMARY KEY,
    role_id UUID REFERENCES roles(id),
    navigation_item_id UUID REFERENCES navigation_items(id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(role_id, navigation_item_id)
);
```

## Seeded Navigation Data

The system comes with pre-seeded navigation data:

### Main Navigation
- Dashboard (/dashboard)
- Alumni (/alumni)
- Events (/events)
- Profile (/profile)

### Administration Navigation
- User Management (/admin/users)
- Role Management (/admin/roles)
- Alumni Management (/admin/alumni)
- Event Management (/admin/events)
- Approvals (/admin/approvals)

### Content Navigation
- Content Management (/admin/content)
- News & Updates (/admin/content/news)
- Resources (/admin/content/resources)

### Analytics Navigation
- Dashboard Analytics (/admin/analytics)
- Alumni Reports (/admin/analytics/alumni)
- Event Reports (/admin/analytics/events)
- Engagement Metrics (/admin/analytics/engagement)

### Settings Navigation
- System Settings (/admin/settings)
- Email Templates (/admin/settings/email)
- Navigation (/admin/settings/navigation)

## Role-Based Access

Different roles have access to different navigation items:

- **SuperAdmin**: All navigation items
- **Admin**: Most navigation except system settings
- **Staff**: Limited navigation (alumni, events, content)
- **Moderator**: Content and approval navigation
- **Alumni**: Basic navigation (dashboard, alumni, events, profile)

## Usage Examples

### Basic Navigation Component
```svelte
<Navigation type="main" className="flex items-center space-x-4" />
```

### Admin Navigation with Groups
```svelte
<Navigation type="admin" showGroups={true} className="space-y-2" />
```

### Mobile Navigation
```svelte
<Navigation type="mobile" className="space-y-1" />
```

### Programmatic Access
```javascript
import { navigationStore } from '$lib/stores/navigation';

// Load user navigation
await navigationStore.loadUserNavigation();

// Get main navigation items
const mainNav = navigationStore.getMainNavigation();

// Get admin navigation items
const adminNav = navigationStore.getAdminNavigation();
```

## Icon Mapping

The system maps icon names to Lucide icons:
- `home` → Home
- `users` → Users
- `calendar` → Calendar
- `file-text` → FileText
- `settings` → Settings
- `bar-chart` → BarChart3
- `shield` → Shield
- `mail` → Mail
- `user` → User
- `plus` → Plus
- `download` → Download
- `image` → Image
- `bell` → Bell
- `graduation-cap` → GraduationCap
- `check-circle` → CheckCircle
- `newspaper` → Newspaper
- `folder` → Folder
- `trending-up` → TrendingUp
- `navigation` → Navigation

## Testing

Run the test script to verify the API:
```bash
node test-navigation.js
```

## Benefits

1. **Dynamic**: Navigation can be changed without code deployment
2. **Role-based**: Different users see different navigation
3. **Hierarchical**: Supports nested navigation items
4. **Icon support**: Visual navigation with icons
5. **Grouped**: Navigation can be organized into groups
6. **Active states**: Automatic highlighting of current page
7. **Responsive**: Works on desktop and mobile

## Future Enhancements

1. **Admin Interface**: Web interface to manage navigation
2. **Drag & Drop**: Visual navigation editor
3. **Conditional Navigation**: Show/hide based on user data
4. **Analytics**: Track navigation usage
5. **Caching**: Cache navigation data for performance
6. **Internationalization**: Multi-language navigation support
