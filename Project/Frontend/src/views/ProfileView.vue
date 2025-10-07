<template>
  <div class="profile-view">
    <!-- 加载状态 -->
    <div v-if="loading" class="loading-container">
      <div class="loading-spinner"></div>
      <p>加载中...</p>
    </div>
    
    <!-- 错误提示 -->
    <div v-else-if="loadError" class="error-container">
      <svg width="48" height="48" viewBox="0 0 16 16">
        <path d="M2.343 13.657A8 8 0 1 1 13.657 2.343 8 8 0 0 1 2.343 13.657ZM6.03 4.97a.751.751 0 0 0-1.042.018.751.751 0 0 0-.018 1.042L6.94 8 4.97 9.97a.749.749 0 0 0 .326 1.275.749.749 0 0 0 .734-.215L8 9.06l2.03 2.03a.751.751 0 0 0 1.042-.018.751.751 0 0 0 .018-1.042L9.06 8l2.03-2.03a.749.749 0 0 0-.326-1.275.749.749 0 0 0-.734.215L8 6.94Z" fill="currentColor"/>
      </svg>
      <p>{{ loadError }}</p>
      <button @click="loadUserProfile" class="retry-btn">重试</button>
    </div>
    
    <!-- 主内容 -->
    <div v-else class="profile-container">
      <!-- 左侧边栏 -->
      <aside class="profile-sidebar">
        <!-- 头像卡片 -->
        <div class="avatar-card">
          <div class="avatar-wrapper">
            <img 
              :src="userProfile.avatar || defaultAvatar" 
              :alt="userProfile.name"
              class="user-avatar"
            />
            <button v-if="isOwnProfile" @click="openAvatarUploadModal" class="edit-avatar-btn" title="更换头像">
              <svg width="16" height="16" viewBox="0 0 16 16">
                <path d="M11.013 1.427a1.75 1.75 0 0 1 2.474 0l1.086 1.086a1.75 1.75 0 0 1 0 2.474l-8.61 8.61c-.21.21-.47.364-.756.445l-3.251.93a.75.75 0 0 1-.927-.928l.929-3.25c.081-.286.235-.547.445-.758l8.61-8.61Zm.176 4.823L9.75 4.81l-6.286 6.287a.253.253 0 0 0-.064.108l-.558 1.953 1.953-.558a.253.253 0 0 0 .108-.064Zm1.238-3.763a.25.25 0 0 0-.354 0L10.811 3.75l1.439 1.44 1.263-1.263a.25.25 0 0 0 0-.354Z" fill="currentColor"/>
              </svg>
            </button>
          </div>
          
          <div class="user-info">
            <h1 class="username">{{ userProfile.name || '未设置昵称' }}</h1>
            <p class="bio">{{ userProfile.bio || '这个人很懒，什么都没写...' }}</p>
          </div>
          
          <button v-if="isOwnProfile" class="edit-profile-btn">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M11.013 1.427a1.75 1.75 0 0 1 2.474 0l1.086 1.086a1.75 1.75 0 0 1 0 2.474l-8.61 8.61c-.21.21-.47.364-.756.445l-3.251.93a.75.75 0 0 1-.927-.928l.929-3.25c.081-.286.235-.547.445-.758l8.61-8.61Zm.176 4.823L9.75 4.81l-6.286 6.287a.253.253 0 0 0-.064.108l-.558 1.953 1.953-.558a.253.253 0 0 0 .108-.064Zm1.238-3.763a.25.25 0 0 0-.354 0L10.811 3.75l1.439 1.44 1.263-1.263a.25.25 0 0 0 0-.354Z" fill="currentColor"/>
            </svg>
            编辑资料
          </button>
        </div>
        
        <!-- 统计数据 -->
        <div class="stats-card">
          <div class="stat-item">
            <div class="stat-icon">
              <svg width="20" height="20" viewBox="0 0 16 16">
                <path d="M0 1.75C0 .784.784 0 1.75 0h12.5C15.216 0 16 .784 16 1.75v9.5A1.75 1.75 0 0 1 14.25 13H8.06l-2.573 2.573A1.458 1.458 0 0 1 3 14.543V13H1.75A1.75 1.75 0 0 1 0 11.25Zm1.75-.25a.25.25 0 0 0-.25.25v9.5c0 .138.112.25.25.25h2a.75.75 0 0 1 .75.75v2.19l2.72-2.72a.749.749 0 0 1 .53-.22h6.5a.25.25 0 0 0 .25-.25v-9.5a.25.25 0 0 0-.25-.25Z" fill="currentColor"/>
              </svg>
            </div>
            <div class="stat-content">
              <span class="stat-value">{{ userStats.totalBlogs }}</span>
              <span class="stat-label">文章</span>
            </div>
          </div>
          
          <div class="stat-item">
            <div class="stat-icon">
              <svg width="20" height="20" viewBox="0 0 16 16">
                <path d="M8 9.5a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z" fill="currentColor"/>
                <path d="M1.679 7.932c.412-.621 1.242-1.75 2.366-2.717C5.175 4.242 6.527 3.5 8 3.5c1.473 0 2.824.742 3.955 1.715 1.124.967 1.954 2.096 2.366 2.717a.119.119 0 0 1 0 .136c-.412.621-1.242 1.75-2.366 2.717C10.825 11.758 9.473 12.5 8 12.5c-1.473 0-2.824-.742-3.955-1.715C2.92 9.818 2.09 8.69 1.679 8.068a.119.119 0 0 1 0-.136Zm12.28.599A.752.752 0 0 0 14 8a.752.752 0 0 0-.041-.531C13.556 6.908 12.63 5.67 11.34 4.58 9.97 3.425 8.344 2.75 8 2.75c-.344 0-1.97.675-3.34 1.83-1.29 1.09-2.216 2.328-2.619 2.889A.752.752 0 0 0 2 8c0 .185.07.373.041.531.403.561 1.329 1.799 2.619 2.889C6.03 12.575 7.656 13.25 8 13.25c.344 0 1.97-.675 3.34-1.83 1.29-1.09 2.216-2.328 2.619-2.889Z" fill="currentColor"/>
              </svg>
            </div>
            <div class="stat-content">
              <span class="stat-value">{{ formatNumber(userStats.totalViews) }}</span>
              <span class="stat-label">浏览</span>
            </div>
          </div>
          
          <div class="stat-item">
            <div class="stat-icon">
              <svg width="20" height="20" viewBox="0 0 16 16">
                <path d="m8 14.25.345.666a.75.75 0 0 1-.69 0l-.008-.004-.018-.01a7.152 7.152 0 0 1-.31-.17 22.055 22.055 0 0 1-3.434-2.414C2.045 10.731 0 8.35 0 5.5 0 2.836 2.086 1 4.25 1 5.797 1 7.153 1.802 8 3.02 8.847 1.802 10.203 1 11.75 1 13.914 1 16 2.836 16 5.5c0 2.85-2.045 5.231-3.885 6.818a22.066 22.066 0 0 1-3.744 2.584l-.018.01-.006.003h-.002ZM4.25 2.5c-1.336 0-2.75 1.164-2.75 3 0 2.15 1.58 4.144 3.365 5.682A20.58 20.58 0 0 0 8 13.393a20.58 20.58 0 0 0 3.135-2.211C12.92 9.644 14.5 7.65 14.5 5.5c0-1.836-1.414-3-2.75-3-1.373 0-2.609.986-3.029 2.456a.749.749 0 0 1-1.442 0C6.859 3.486 5.623 2.5 4.25 2.5Z" fill="currentColor"/>
              </svg>
            </div>
            <div class="stat-content">
              <span class="stat-value">{{ formatNumber(userStats.totalLikes) }}</span>
              <span class="stat-label">获赞</span>
            </div>
          </div>
        </div>
        
        <!-- 社交链接 -->
        <div class="social-card">
          <h3 class="card-title">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="m7.775 3.275 1.25-1.25a3.5 3.5 0 1 1 4.95 4.95l-2.5 2.5a3.5 3.5 0 0 1-4.95 0 .751.751 0 0 1 .018-1.042.751.751 0 0 1 1.042-.018 1.998 1.998 0 0 0 2.83 0l2.5-2.5a2.002 2.002 0 0 0-2.83-2.83l-1.25 1.25a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042Zm-4.69 9.64a1.998 1.998 0 0 0 2.83 0l1.25-1.25a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042l-1.25 1.25a3.5 3.5 0 1 1-4.95-4.95l2.5-2.5a3.5 3.5 0 0 1 4.95 0 .751.751 0 0 1-.018 1.042.751.751 0 0 1-1.042.018 1.998 1.998 0 0 0-2.83 0l-2.5 2.5a1.998 1.998 0 0 0 0 2.83Z" fill="currentColor"/>
            </svg>
            社交链接
          </h3>
          
          <div class="social-links">
            <a v-if="userProfile.github" :href="userProfile.github" target="_blank" class="social-link">
              <svg width="20" height="20" viewBox="0 0 16 16">
                <path d="M8 0c4.42 0 8 3.58 8 8a8.013 8.013 0 0 1-5.45 7.59c-.4.08-.55-.17-.55-.38 0-.27.01-1.13.01-2.2 0-.75-.25-1.23-.54-1.48 1.78-.2 3.65-.88 3.65-3.95 0-.88-.31-1.59-.82-2.15.08-.2.36-1.02-.08-2.12 0 0-.67-.22-2.2.82-.64-.18-1.32-.27-2-.27-.68 0-1.36.09-2 .27-1.53-1.03-2.2-.82-2.2-.82-.44 1.1-.16 1.92-.08 2.12-.51.56-.82 1.28-.82 2.15 0 3.06 1.86 3.75 3.64 3.95-.23.2-.44.55-.51 1.07-.46.21-1.61.55-2.33-.66-.15-.24-.6-.83-1.23-.82-.67.01-.27.38.01.53.34.19.73.9.82 1.13.16.45.68 1.31 2.69.94 0 .67.01 1.3.01 1.49 0 .21-.15.45-.55.38A7.995 7.995 0 0 1 0 8c0-4.42 3.58-8 8-8Z" fill="currentColor"/>
              </svg>
              <span>GitHub</span>
            </a>
            
            <a v-if="userProfile.twitter" :href="userProfile.twitter" target="_blank" class="social-link">
              <svg width="20" height="20" viewBox="0 0 16 16">
                <path d="M12.6.75h2.454l-5.36 6.142L16 15.25h-4.937l-3.867-5.07-4.425 5.07H.316l5.733-6.57L0 .75h5.063l3.495 4.633L12.601.75Zm-.86 13.028h1.36L4.323 2.145H2.865l8.875 11.633Z" fill="currentColor"/>
              </svg>
              <span>Twitter</span>
            </a>
            
            <a v-if="userProfile.email" :href="`mailto:${userProfile.email}`" class="social-link">
              <svg width="20" height="20" viewBox="0 0 16 16">
                <path d="M1.75 2h12.5c.966 0 1.75.784 1.75 1.75v8.5A1.75 1.75 0 0 1 14.25 14H1.75A1.75 1.75 0 0 1 0 12.25v-8.5C0 2.784.784 2 1.75 2ZM1.5 12.251c0 .138.112.25.25.25h12.5a.25.25 0 0 0 .25-.25V5.809L8.38 9.397a.75.75 0 0 1-.76 0L1.5 5.809v6.442Zm13-8.181v-.32a.25.25 0 0 0-.25-.25H1.75a.25.25 0 0 0-.25.25v.32L8 7.88Z" fill="currentColor"/>
              </svg>
              <span>Email</span>
            </a>
            
            <a v-if="userProfile.website" :href="userProfile.website" target="_blank" class="social-link">
              <svg width="20" height="20" viewBox="0 0 16 16">
                <path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM5.78 8.75a9.64 9.64 0 0 0 1.363 4.177c.255.426.542.832.857 1.215.245-.296.551-.705.857-1.215A9.64 9.64 0 0 0 10.22 8.75Zm4.44-1.5a9.64 9.64 0 0 0-1.363-4.177c-.307-.51-.612-.919-.857-1.215a9.927 9.927 0 0 0-.857 1.215A9.64 9.64 0 0 0 5.78 7.25Zm-5.944 1.5H1.543a6.507 6.507 0 0 0 4.666 5.5c-.123-.181-.24-.365-.352-.552-.715-1.192-1.437-2.874-1.581-4.948Zm-2.733-1.5h2.733c.144-2.074.866-3.756 1.58-4.948.12-.197.237-.381.353-.552a6.507 6.507 0 0 0-4.666 5.5Zm10.181 1.5c-.144 2.074-.866 3.756-1.58 4.948-.12.197-.237.381-.353.552a6.507 6.507 0 0 0 4.666-5.5Zm2.733-1.5a6.507 6.507 0 0 0-4.666-5.5c.123.181.24.365.353.552.714 1.192 1.436 2.874 1.58 4.948Z" fill="currentColor"/>
              </svg>
              <span>Website</span>
            </a>
          </div>
        </div>
        
        <!-- 技能标签 -->
        <div v-if="userProfile.skills" class="skills-card">
          <h3 class="card-title">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M1 7.775V2.75C1 1.784 1.784 1 2.75 1h5.025c.464 0 .91.184 1.238.513l6.25 6.25a1.75 1.75 0 0 1 0 2.474l-5.026 5.026a1.75 1.75 0 0 1-2.474 0l-6.25-6.25A1.752 1.752 0 0 1 1 7.775Zm1.5 0c0 .066.026.13.073.177l6.25 6.25a.25.25 0 0 0 .354 0l5.025-5.025a.25.25 0 0 0 0-.354l-6.25-6.25a.25.25 0 0 0-.177-.073H2.75a.25.25 0 0 0-.25.25ZM6 5a1 1 0 1 1 0 2 1 1 0 0 1 0-2Z" fill="currentColor"/>
            </svg>
            技能标签
          </h3>
          
          <div class="skill-tags">
            <span 
              v-for="skill in userProfile.skills.split(',').map(s => s.trim()).filter(s => s)" 
              :key="skill" 
              class="skill-tag"
            >
              {{ skill }}
            </span>
          </div>
        </div>
      </aside>
      
      <!-- 右侧主内容区 -->
      <main class="profile-main">
        <!-- 个人简介 -->
        <section v-if="userProfile.introduction" class="intro-section">
          <div class="section-header">
            <h2 class="section-title">
              <svg width="20" height="20" viewBox="0 0 16 16">
                <path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Zm9.78-2.22-5.5 5.5a.749.749 0 0 1-1.275-.326.749.749 0 0 1 .215-.734l5.5-5.5a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042Z" fill="currentColor"/>
              </svg>
              关于我
            </h2>
          </div>
          <div class="intro-content markdown-body" v-html="renderMarkdown(userProfile.introduction)"></div>
        </section>
        
        <!-- 技术栈 -->
        <section class="tech-stack-section">
          <div class="section-header">
            <h2 class="section-title">
              <svg width="20" height="20" viewBox="0 0 16 16">
                <path d="M0 1.75C0 .784.784 0 1.75 0h12.5C15.216 0 16 .784 16 1.75v12.5A1.75 1.75 0 0 1 14.25 16H1.75A1.75 1.75 0 0 1 0 14.25Zm1.75-.25a.25.25 0 0 0-.25.25v12.5c0 .138.112.25.25.25h12.5a.25.25 0 0 0 .25-.25V1.75a.25.25 0 0 0-.25-.25ZM7.5 6.5v1h1v-1h-1Zm-2 0v1h1v-1h-1Zm-2 0v1h1v-1h-1Z" fill="currentColor"/>
              </svg>
              技术栈
            </h2>
          </div>
          
          <div class="tech-stack-grid">
            <div v-for="tech in techStack" :key="tech.name" class="tech-item">
              <div class="tech-header">
                <span class="tech-name">{{ tech.name }}</span>
                <span class="tech-percentage">{{ tech.level }}%</span>
              </div>
              <div class="tech-progress-bar">
                <div class="tech-progress-fill" :style="{ width: tech.level + '%' }"></div>
              </div>
            </div>
          </div>
        </section>
        
        <!-- 贡献热力图 -->
        <section class="contributions-section">
          <div class="section-header">
            <h2 class="section-title">
              <svg width="20" height="20" viewBox="0 0 16 16">
                <path d="M0 1.75C0 .784.784 0 1.75 0h12.5C15.216 0 16 .784 16 1.75v12.5A1.75 1.75 0 0 1 14.25 16H1.75A1.75 1.75 0 0 1 0 14.25Zm1.75-.25a.25.25 0 0 0-.25.25v12.5c0 .138.112.25.25.25h12.5a.25.25 0 0 0 .25-.25V1.75a.25.25 0 0 0-.25-.25Z" fill="currentColor"/>
              </svg>
              {{ currentYear }} 年贡献
            </h2>
            <span class="contribution-count">{{ totalContributions }} 篇文章</span>
          </div>
          
          <div class="contributions-calendar">
            <div class="calendar-months">
              <span v-for="month in months" :key="month">{{ month }}</span>
            </div>
            <div class="calendar-grid">
              <div v-for="week in contributions" :key="week.week" class="calendar-week">
                <div 
                  v-for="day in week.days" 
                  :key="day.date"
                  class="calendar-day"
                  :class="`level-${day.level}`"
                  :title="`${day.date}: ${day.count} 篇文章`"
                ></div>
              </div>
            </div>
            <div class="calendar-legend">
              <span class="legend-label">少</span>
              <div class="legend-levels">
                <span class="legend-level level-0"></span>
                <span class="legend-level level-1"></span>
                <span class="legend-level level-2"></span>
                <span class="legend-level level-3"></span>
                <span class="legend-level level-4"></span>
              </div>
              <span class="legend-label">多</span>
            </div>
          </div>
        </section>
        
        <!-- 热门文章 -->
        <section class="popular-blogs-section">
          <div class="section-header">
            <h2 class="section-title">
              <svg width="20" height="20" viewBox="0 0 16 16">
                <path d="M8 4a.75.75 0 0 1 .75.75V8.5h3.75a.75.75 0 0 1 0 1.5h-4.5A.75.75 0 0 1 7.25 9V4.75A.75.75 0 0 1 8 4Z" fill="currentColor"/>
                <path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Z" fill="currentColor"/>
              </svg>
              {{ sectionTitle }}
            </h2>
            
            <!-- 排序选择器 -->
            <CustomSelect 
              v-model="sortBy"
              :options="sortOptions"
            />
          </div>
          
<<<<<<< Updated upstream
          <div class="blog-list">
            <div v-for="blog in popularBlogs" :key="blog.id" class="blog-item" @click="goToBlog(blog.id)">
              <div class="blog-header">
                <h3 class="blog-title">{{ blog.title }}</h3>
                <span class="blog-date">{{ formatDate(blog.publishedAt) }}</span>
              </div>
              <p class="blog-excerpt">{{ blog.excerpt }}</p>
              <div class="blog-meta">
                <span class="meta-item">
                  <svg width="14" height="14" viewBox="0 0 16 16">
                    <path d="M8 9.5a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z" fill="currentColor"/>
                    <path d="M1.679 7.932c.412-.621 1.242-1.75 2.366-2.717C5.175 4.242 6.527 3.5 8 3.5c1.473 0 2.824.742 3.955 1.715 1.124.967 1.954 2.096 2.366 2.717a.119.119 0 0 1 0 .136c-.412.621-1.242 1.75-2.366 2.717C10.825 11.758 9.473 12.5 8 12.5c-1.473 0-2.824-.742-3.955-1.715C2.92 9.818 2.09 8.69 1.679 8.068a.119.119 0 0 1 0-.136Z" fill="currentColor"/>
                  </svg>
                  {{ formatNumber(blog.views) }}
                </span>
                <span class="meta-item">
                  <svg width="14" height="14" viewBox="0 0 16 16">
                    <path d="M0 1.75C0 .784.784 0 1.75 0h12.5C15.216 0 16 .784 16 1.75v9.5A1.75 1.75 0 0 1 14.25 13H8.06l-2.573 2.573A1.458 1.458 0 0 1 3 14.543V13H1.75A1.75 1.75 0 0 1 0 11.25Zm1.75-.25a.25.25 0 0 0-.25.25v9.5c0 .138.112.25.25.25h2a.75.75 0 0 1 .75.75v2.19l2.72-2.72a.749.749 0 0 1 .53-.22h6.5a.25.25 0 0 0 .25-.25v-9.5a.25.25 0 0 0-.25-.25Z" fill="currentColor"/>
                  </svg>
                  {{ blog.commentCount }}
                </span>
                <div class="blog-tags">
                  <span v-for="tag in blog.tags.split(',').slice(0, 2)" :key="tag" class="blog-tag">
                    {{ tag.trim() }}
                  </span>
                </div>
              </div>
            </div>
=======
          <!-- 使用 ContentPage 组件展示热门文章 -->
          <ContentPage
            v-if="popularBlogItems.length > 0"
            :items="popularBlogItems"
            :items-per-row="1"
            :items-per-page="3"
            :sort-by="sortBy"
            @item-click="goToBlog"
          />
          
          <!-- 空状态 -->
          <div v-else class="empty-blogs">
            <p>暂无文章</p>
>>>>>>> Stashed changes
          </div>
        </section>
      </main>
    </div>
    
    <!-- 头像上传弹窗 -->
    <div v-if="showAvatarUploadModal" class="modal-overlay" @click="showAvatarUploadModal = false">
      <div class="modal-content" @click.stop>
        <div class="modal-header">
          <h3>
            <svg width="20" height="20" viewBox="0 0 16 16">
              <path d="M1.75 2.5a.25.25 0 0 0-.25.25v10.5c0 .138.112.25.25.25h.94a.76.76 0 0 1 .03-.03l6.077-6.078a1.75 1.75 0 0 1 2.412-.06L14.5 10.31V2.75a.25.25 0 0 0-.25-.25Zm12.5-1H1.75C.784 1.5 0 2.284 0 3.25v9.5C0 13.216.784 14 1.75 14h12.5A1.75 1.75 0 0 0 16 12.25v-9.5A1.75 1.75 0 0 0 14.25 1.5Z" fill="currentColor"/>
            </svg>
            更换头像
          </h3>
          <button @click="showAvatarUploadModal = false" class="modal-close">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M3.72 3.72a.75.75 0 0 1 1.06 0L8 6.94l3.22-3.22a.749.749 0 0 1 1.275.326.749.749 0 0 1-.215.734L9.06 8l3.22 3.22a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L8 9.06l-3.22 3.22a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L6.94 8 3.72 4.78a.75.75 0 0 1 0-1.06Z" fill="currentColor"/>
            </svg>
          </button>
        </div>
        <div class="modal-body">
          <ImageUpload 
            ref="avatarUploadRef"
            @file-selected="handleAvatarSelected" 
            image-alt="用户头像"
          />
          
          <div class="upload-tips">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8Zm8-6.5a6.5 6.5 0 1 0 0 13 6.5 6.5 0 0 0 0-13ZM6.5 7.75A.75.75 0 0 1 7.25 7h1a.75.75 0 0 1 .75.75v2.75h.25a.75.75 0 0 1 0 1.5h-2a.75.75 0 0 1 0-1.5h.25v-2h-.25a.75.75 0 0 1-.75-.75ZM8 6a1 1 0 1 1 0-2 1 1 0 0 1 0 2Z" fill="currentColor"/>
            </svg>
            <div class="tips-text">
              <p>• 建议上传正方形图片，最佳尺寸 400x400</p>
              <p>• 支持 JPG、PNG、GIF、WebP 格式</p>
              <p>• 文件大小不超过 5MB</p>
            </div>
          </div>
          
          <button 
            @click="uploadAvatar" 
            class="upload-confirm-btn"
            :disabled="!hasSelectedAvatar || uploading"
          >
            <svg v-if="!uploading" width="16" height="16" viewBox="0 0 16 16">
              <path d="M13.78 4.22a.75.75 0 0 1 0 1.06l-7.25 7.25a.75.75 0 0 1-1.06 0L2.22 9.28a.751.751 0 0 1 .018-1.042.751.751 0 0 1 1.042-.018L6 10.94l6.72-6.72a.75.75 0 0 1 1.06 0Z" fill="currentColor"/>
            </svg>
            <span v-if="uploading" class="spinner"></span>
            {{ uploading ? '上传中...' : '确认更换' }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, watch } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { marked } from 'marked'
import DOMPurify from 'dompurify'
import ImageUpload from '@/components/ImageUpload.vue'
<<<<<<< Updated upstream
=======
import ContentPage from '@/components/ContentPage.vue'
import CustomSelect from '@/components/CustomSelect.vue'
>>>>>>> Stashed changes

const router = useRouter()
const route = useRoute()

// 加载状态
const loading = ref(true)
const loadError = ref('')

// 默认头像
const defaultAvatar = 'https://avatars.githubusercontent.com/u/0?v=4'

// 当前年份
const currentYear = new Date().getFullYear()

// 头像上传相关
const showAvatarUploadModal = ref(false)
const avatarUploadRef = ref<InstanceType<typeof ImageUpload> | null>(null)
const hasSelectedAvatar = ref(false)
const uploading = ref(false)
const selectedAvatarFile = ref<File | null>(null)
const selectedAvatarBlobUrl = ref('')

<<<<<<< Updated upstream
=======
// 编辑资料相关
const showEditProfileModal = ref(false)
const editForm = ref({
  name: '',
  bio: '',
  introduction: '',
  github: '',
  twitter: '',
  website: '',
  skills: ''
})
const saving = ref(false)

// 排序方式
const sortBy = ref<'time-desc' | 'time-asc' | 'views-desc' | 'views-asc' | 'likes-desc' | 'likes-asc' | 'comments-desc' | 'comments-asc' | 'none'>('time-desc')

// 排序选项
const sortOptions = [
  { label: '最新发布', value: 'time-desc' },
  { label: '最多浏览', value: 'views-desc' },
  { label: '最多点赞', value: 'likes-desc' },
  { label: '最多评论', value: 'comments-desc' }
]

// 根据排序方式动态生成标题
const sectionTitle = computed(() => {
  switch (sortBy.value) {
    case 'time-desc':
      return '最新文章'
    case 'time-asc':
      return '最早文章'
    case 'views-desc':
      return '最多浏览'
    case 'views-asc':
      return '最少浏览'
    case 'likes-desc':
      return '最多点赞'
    case 'likes-asc':
      return '最少点赞'
    case 'comments-desc':
      return '最多评论'
    case 'comments-asc':
      return '最少评论'
    default:
      return '热门文章'
  }
})

>>>>>>> Stashed changes
// 用户资料
const userProfile = ref({
  id: '',
  name: '',
  email: '',
  avatar: '',
  bio: '',
  introduction: '',
  github: '',
  twitter: '',
  website: '',
  skills: '',
  registerTime: ''
})

// 是否是本人资料
const isOwnProfile = computed(() => {
  const currentUserId = localStorage.getItem('userId')
  return currentUserId === userProfile.value.id
})

// 用户统计
const userStats = ref({
  totalBlogs: 0,
  totalViews: 0,
  totalLikes: 0
})

// 技术栈(带熟练度) - 从 skills 字段解析而来
const techStack = computed(() => {
  if (!userProfile.value.skills) return []
  
  // 将逗号分隔的字符串转换为数组
  const skillsArray = userProfile.value.skills.split(',').map(s => s.trim()).filter(s => s)
  
  // 生成带随机熟练度的技术栈(70-95之间)
  return skillsArray.map(skill => ({
    name: skill,
    level: Math.floor(Math.random() * 26) + 70 // 70-95
  }))
})

// 月份标签
const months = ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']

// 贡献数据(模拟)
const contributions = ref<any[]>([])
const totalContributions = ref(0)

// 生成贡献热力图数据
const generateContributions = () => {
  const weeks = []
  const today = new Date()
  const startDate = new Date(today.getFullYear(), 0, 1)
  
  let weekData = { week: 0, days: [] }
  let currentDate = new Date(startDate)
  let weekCount = 0
  let total = 0
  
  // 生成52周的数据
  for (let i = 0; i < 365; i++) {
    const count = Math.random() > 0.7 ? Math.floor(Math.random() * 5) : 0
    const level = count === 0 ? 0 : count <= 1 ? 1 : count <= 2 ? 2 : count <= 3 ? 3 : 4
    
    weekData.days.push({
      date: currentDate.toISOString().split('T')[0],
      count,
      level
    })
    
    total += count
    
    if (currentDate.getDay() === 6 || i === 364) {
      weeks.push({ ...weekData, week: weekCount })
      weekData = { week: weekCount + 1, days: [] }
      weekCount++
    }
    
    currentDate.setDate(currentDate.getDate() + 1)
  }
  
  contributions.value = weeks
  totalContributions.value = total
}

// 热门文章
const popularBlogs = ref<any[]>([])

<<<<<<< Updated upstream
=======
// 转换为 ContentPage 需要的格式
const popularBlogItems = computed(() => {
  return popularBlogs.value.map(blog => ({
    id: blog.id,
    title: blog.title,
    description: blog.content.substring(0, 120) + '...',
    icon: 'book',
    tags: blog.tags ? blog.tags.split(',').slice(0, 3).map((t: string) => t.trim()) : [],
    stats: {
      views: blog.views,
      likes: blog.likes,
      comments: blog.commentsCount
    },
    updateTime: blog.createdAt, // 保留原始日期字符串用于排序
    author: blog.authorId ? {
      id: blog.authorId,
      name: blog.authorName || '未知作者',
      avatar: blog.authorAvatar
    } : undefined
  }))
})

>>>>>>> Stashed changes
// 格式化数字
const formatNumber = (num: number): string => {
  if (num >= 10000) {
    return (num / 10000).toFixed(1) + 'w'
  } else if (num >= 1000) {
    return (num / 1000).toFixed(1) + 'k'
  }
  return num.toString()
}

// 格式化日期
const formatDate = (dateString: string): string => {
  const date = new Date(dateString)
  const now = new Date()
  const diff = now.getTime() - date.getTime()
  const days = Math.floor(diff / (1000 * 60 * 60 * 24))
  
  if (days === 0) return '今天'
  if (days === 1) return '昨天'
  if (days < 7) return `${days}天前`
  if (days < 30) return `${Math.floor(days / 7)}周前`
  if (days < 365) return `${Math.floor(days / 30)}个月前`
  return `${Math.floor(days / 365)}年前`
}

// 渲染Markdown
const renderMarkdown = (content: string): string => {
  if (!content) return ''
  
  try {
    const rawHtml = marked.parse(content) as string
    return DOMPurify.sanitize(rawHtml, {
      ALLOWED_TAGS: ['p', 'br', 'strong', 'em', 'u', 's', 'code', 'pre', 'a', 'ul', 'ol', 'li', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6', 'blockquote', 'hr'],
      ALLOWED_ATTR: ['href', 'target', 'rel']
    })
  } catch (error) {
    console.error('Markdown渲染失败:', error)
    return content
  }
}

// 跳转到博客详情
const goToBlog = (blogId: string) => {
  router.push(`/blog/${blogId}`)
}

// 打开头像上传弹窗
const openAvatarUploadModal = () => {
  showAvatarUploadModal.value = true
  hasSelectedAvatar.value = false
  selectedAvatarFile.value = null
  selectedAvatarBlobUrl.value = ''
}

// 处理头像文件选择
const handleAvatarSelected = (file: File, blobUrl: string) => {
  selectedAvatarFile.value = file
  selectedAvatarBlobUrl.value = blobUrl
  hasSelectedAvatar.value = true
}

// 上传头像
const uploadAvatar = async () => {
  if (!selectedAvatarFile.value) {
    alert('请先选择头像')
    return
  }
  
  uploading.value = true
  
  try {
    // 第一步: 上传新头像 (指定 category 为 avatar)
    const formData = new FormData()
    formData.append('file', selectedAvatarFile.value)
    
    const uploadResponse = await fetch('/api/File/upload?category=avatar', {
      method: 'POST',
      body: formData
    })
    
    if (!uploadResponse.ok) {
      throw new Error('头像上传失败')
    }
    
    const uploadResult = await uploadResponse.json()
    const newAvatarUrl = `https://localhost:7005${uploadResult.url}`
    
    // 第二步: 如果有旧头像且是本地上传的,删除旧头像
    if (userProfile.value.avatar) {
      try {
        // 提取相对路径 (如 /images/avatars/xxx.jpg 或 /uploads/xxx.jpg)
        const oldAvatarPath = new URL(userProfile.value.avatar).pathname
        
        await fetch(`/api/File?fileUrl=${encodeURIComponent(oldAvatarPath)}`, {
          method: 'DELETE'
        })
        console.log('旧头像已删除:', oldAvatarPath)
      } catch (error) {
        console.warn('删除旧头像失败:', error)
        // 删除失败不影响主流程
      }
    }
    
    // 第三步: 更新用户资料
    const userId = userProfile.value.id
    const updateResponse = await fetch(`/api/UserData/${userId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        avatar: newAvatarUrl
      })
    })
    
    if (!updateResponse.ok) {
      throw new Error('更新用户资料失败')
    }
    
    // 更新本地数据
    userProfile.value.avatar = newAvatarUrl
    
    // 保存到 localStorage
    localStorage.setItem('userProfile', JSON.stringify(userProfile.value))
    
    // 释放 blob URL
    if (selectedAvatarBlobUrl.value) {
      URL.revokeObjectURL(selectedAvatarBlobUrl.value)
    }
    
    // 关闭弹窗
    showAvatarUploadModal.value = false
    hasSelectedAvatar.value = false
    
    alert('头像更新成功!')
    
  } catch (error) {
    console.error('头像上传失败:', error)
    alert(error instanceof Error ? error.message : '头像上传失败,请重试')
  } finally {
    uploading.value = false
  }
}

// 加载用户数据
const loadUserProfile = async () => {
  loading.value = true
  loadError.value = ''
  
  try {
    // 从路由参数获取用户ID,如果没有则使用当前登录用户ID
    let userId = route.params.userId as string
    
    if (!userId) {
      // 如果路由没有提供 userId,使用当前登录用户的 ID
      userId = localStorage.getItem('userId') || ''
      if (!userId) {
        loadError.value = '未找到用户ID，请先登录'
        console.error('未找到用户ID')
        return
      }
    }
    
    // 1. 加载用户详细信息
    const userResponse = await fetch(`/api/UserData/details/${userId}`)
    if (userResponse.ok) {
      const userData = await userResponse.json()
      // 更新用户资料
      userProfile.value = {
        id: userData.id,
        name: userData.name,
        email: userData.email,
        avatar: userData.avatar || '',
        bio: userData.bio || '',
        introduction: userData.introduction || '',
        github: userData.github || '',
        twitter: userData.twitter || '',
        website: userData.website || '',
        skills: userData.skills || '',
        registerTime: userData.registerTime
      }
      
      // 只有查看自己的资料时才更新 localStorage
      const currentUserId = localStorage.getItem('userId')
      if (userId === currentUserId) {
        localStorage.setItem('userProfile', JSON.stringify(userProfile.value))
      }
    } else {
      loadError.value = '获取用户详情失败'
      console.error('获取用户详情失败:', userResponse.status)
    }
    
    // 2. 加载用户统计
    const statsResponse = await fetch(`/api/UserData/stats/${userId}`)
    if (statsResponse.ok) {
      const stats = await statsResponse.json()
      userStats.value = {
        totalBlogs: stats.totalBlogs || 0,
        totalViews: stats.totalViews || 0,
        totalLikes: stats.totalLikes || 0
      }
    } else {
      console.warn('获取用户统计失败')
    }
    
    // 3. 加载热门文章 (按浏览量排序,取前5篇)
    const blogsResponse = await fetch(`/api/Blog/user/${userId}?pageSize=5&sortBy=views`)
    if (blogsResponse.ok) {
      const result = await blogsResponse.json()
      popularBlogs.value = result.blogs.map((blog: any) => ({
        ...blog,
        excerpt: blog.content.substring(0, 100) + '...'
      }))
    } else {
      console.warn('获取热门文章失败')
    }
  } catch (error) {
    console.error('加载用户资料失败:', error)
    loadError.value = '加载失败，请稍后重试'
  } finally {
    loading.value = false
  }
}

// 监听路由参数变化,重新加载用户数据
watch(() => route.params.userId, (newUserId, oldUserId) => {
  if (newUserId !== oldUserId) {
    loadUserProfile()
    generateContributions()
  }
})

onMounted(() => {
  loadUserProfile()
  generateContributions()
})
</script>

<style scoped>
.profile-view {
  min-height: 100vh;
  background: linear-gradient(135deg, #0d1117 0%, #1a1f2e 100%);
  padding: 2rem 0;
}

/* 加载状态 */
.loading-container,
.error-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  min-height: 60vh;
  color: #c9d1d9;
  gap: 1.5rem;
}

.loading-spinner {
  width: 48px;
  height: 48px;
  border: 4px solid #30363d;
  border-top-color: #58a6ff;
  border-radius: 50%;
  animation: spin 1s linear infinite;
}

@keyframes spin {
  to { transform: rotate(360deg); }
}

.error-container svg {
  color: #f85149;
}

.retry-btn {
  padding: 0.75rem 1.5rem;
  background: #21262d;
  border: 1px solid #30363d;
  border-radius: 8px;
  color: #c9d1d9;
  font-size: 0.95rem;
  cursor: pointer;
  transition: all 0.2s ease;
}

.retry-btn:hover {
  background: #30363d;
  border-color: #58a6ff;
  color: #58a6ff;
}

.profile-container {
  max-width: 1400px;
  margin: 0 auto;
  padding: 0 2rem;
  display: grid;
  grid-template-columns: 360px 1fr;
  gap: 2rem;
}

/* ========== 左侧边栏 ========== */
.profile-sidebar {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

/* 头像卡片 */
.avatar-card {
  background: rgba(22, 27, 34, 0.8);
  backdrop-filter: blur(10px);
  border: 1px solid #30363d;
  border-radius: 12px;
  padding: 2rem;
  text-align: center;
  transition: all 0.3s ease;
}

.avatar-card:hover {
  border-color: #58a6ff;
  box-shadow: 0 0 20px rgba(88, 166, 255, 0.2);
}

.avatar-wrapper {
  position: relative;
  width: 200px;
  height: 200px;
  margin: 0 auto 1.5rem;
}

.user-avatar {
  width: 100%;
  height: 100%;
  border-radius: 50%;
  border: 4px solid #30363d;
  transition: all 0.3s ease;
  object-fit: cover;
}

.avatar-wrapper:hover .user-avatar {
  border-color: #58a6ff;
  transform: scale(1.05);
  box-shadow: 0 0 30px rgba(88, 166, 255, 0.4);
}

.edit-avatar-btn {
  position: absolute;
  bottom: 10px;
  right: 10px;
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background: #21262d;
  border: 2px solid #30363d;
  color: #8b949e;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  transition: all 0.3s ease;
}

.edit-avatar-btn:hover {
  background: #58a6ff;
  border-color: #58a6ff;
  color: white;
  transform: scale(1.1);
}

.user-info {
  margin-bottom: 1.5rem;
}

.username {
  font-size: 1.75rem;
  font-weight: 600;
  color: #c9d1d9;
  margin: 0 0 0.5rem 0;
}

.bio {
  color: #8b949e;
  font-size: 0.95rem;
  line-height: 1.5;
  margin: 0;
}

.edit-profile-btn {
  width: 100%;
  padding: 0.75rem 1rem;
  background: transparent;
  border: 1px solid #30363d;
  border-radius: 8px;
  color: #c9d1d9;
  font-size: 0.95rem;
  font-weight: 500;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 0.5rem;
  transition: all 0.3s ease;
}

.edit-profile-btn:hover {
  background: rgba(88, 166, 255, 0.1);
  border-color: #58a6ff;
  color: #58a6ff;
}

/* 统计卡片 */
.stats-card {
  background: rgba(22, 27, 34, 0.8);
  backdrop-filter: blur(10px);
  border: 1px solid #30363d;
  border-radius: 12px;
  padding: 1.5rem;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.stat-item {
  display: flex;
  align-items: center;
  gap: 1rem;
  padding: 1rem;
  background: rgba(13, 17, 23, 0.6);
  border-radius: 8px;
  transition: all 0.3s ease;
}

.stat-item:hover {
  background: rgba(88, 166, 255, 0.1);
  transform: translateX(5px);
}

.stat-icon {
  width: 40px;
  height: 40px;
  border-radius: 8px;
  background: linear-gradient(135deg, #58a6ff 0%, #1f6feb 100%);
  display: flex;
  align-items: center;
  justify-content: center;
  color: white;
}

.stat-content {
  display: flex;
  flex-direction: column;
  flex: 1;
}

.stat-value {
  font-size: 1.5rem;
  font-weight: 600;
  color: #c9d1d9;
}

.stat-label {
  font-size: 0.875rem;
  color: #8b949e;
}

/* 社交链接卡片 */
.social-card,
.skills-card {
  background: rgba(22, 27, 34, 0.8);
  backdrop-filter: blur(10px);
  border: 1px solid #30363d;
  border-radius: 12px;
  padding: 1.5rem;
}

.card-title {
  font-size: 1rem;
  font-weight: 600;
  color: #c9d1d9;
  margin: 0 0 1rem 0;
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.card-title svg {
  color: #58a6ff;
}

.social-links {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.social-link {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  padding: 0.75rem 1rem;
  background: rgba(13, 17, 23, 0.6);
  border-radius: 8px;
  color: #8b949e;
  text-decoration: none;
  transition: all 0.3s ease;
}

.social-link:hover {
  background: rgba(88, 166, 255, 0.1);
  color: #58a6ff;
  transform: translateX(5px);
}

.social-link svg {
  flex-shrink: 0;
}

/* 技能标签 */
.skill-tags {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
}

.skill-tag {
  padding: 0.5rem 1rem;
  background: rgba(88, 166, 255, 0.1);
  border: 1px solid #1f6feb;
  border-radius: 20px;
  color: #58a6ff;
  font-size: 0.875rem;
  font-weight: 500;
  transition: all 0.3s ease;
}

.skill-tag:hover {
  background: rgba(88, 166, 255, 0.2);
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(88, 166, 255, 0.3);
}

/* ========== 右侧主内容区 ========== */
.profile-main {
  display: flex;
  flex-direction: column;
  gap: 2rem;
}

/* 通用section样式 */
.intro-section,
.tech-stack-section,
.contributions-section,
.popular-blogs-section {
  background: rgba(22, 27, 34, 0.8);
  backdrop-filter: blur(10px);
  border: 1px solid #30363d;
  border-radius: 12px;
  padding: 2rem;
  transition: all 0.3s ease;
}

.intro-section:hover,
.tech-stack-section:hover,
.contributions-section:hover,
.popular-blogs-section:hover {
  border-color: #58a6ff;
  box-shadow: 0 0 20px rgba(88, 166, 255, 0.15);
}

.section-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 1.5rem;
  padding-bottom: 1rem;
  border-bottom: 1px solid #30363d;
  flex-wrap: wrap;
  gap: 1rem;
}

.section-title {
  font-size: 1.25rem;
  font-weight: 600;
  color: #c9d1d9;
  margin: 0;
  display: flex;
  align-items: center;
  gap: 0.75rem;
}

.section-title svg {
  color: #58a6ff;
}

/* 个人简介 */
.intro-content {
  color: #c9d1d9;
  line-height: 1.8;
}

.intro-content :deep(h2) {
  color: #c9d1d9;
  font-size: 1.25rem;
  margin: 1.5rem 0 1rem 0;
  padding-bottom: 0.5rem;
  border-bottom: 1px solid #30363d;
}

.intro-content :deep(h3) {
  color: #c9d1d9;
  font-size: 1.1rem;
  margin: 1rem 0 0.75rem 0;
}

.intro-content :deep(p) {
  margin: 0.75rem 0;
}

.intro-content :deep(ul) {
  margin: 0.75rem 0;
  padding-left: 1.5rem;
}

.intro-content :deep(li) {
  margin: 0.5rem 0;
}

.intro-content :deep(strong) {
  color: #58a6ff;
}

.intro-content :deep(blockquote) {
  margin: 1rem 0;
  padding: 0.75rem 1rem;
  border-left: 4px solid #58a6ff;
  background: rgba(88, 166, 255, 0.05);
  color: #8b949e;
  font-style: italic;
}

/* 技术栈 */
.tech-stack-grid {
  display: flex;
  flex-direction: column;
  gap: 1.25rem;
}

.tech-item {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.tech-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.tech-name {
  font-size: 0.95rem;
  font-weight: 500;
  color: #c9d1d9;
}

.tech-percentage {
  font-size: 0.875rem;
  font-weight: 600;
  color: #58a6ff;
}

.tech-progress-bar {
  height: 8px;
  background: rgba(48, 54, 61, 0.5);
  border-radius: 4px;
  overflow: hidden;
}

.tech-progress-fill {
  height: 100%;
  background: linear-gradient(90deg, #58a6ff 0%, #1f6feb 100%);
  border-radius: 4px;
  transition: width 1s ease;
}

/* 贡献热力图 */
.contribution-count {
  font-size: 0.875rem;
  color: #8b949e;
}

.contributions-calendar {
  margin-top: 1rem;
}

.calendar-months {
  display: flex;
  gap: 1rem;
  margin-bottom: 0.5rem;
  font-size: 0.75rem;
  color: #8b949e;
  padding-left: 2rem;
}

.calendar-months span {
  flex: 1;
  text-align: center;
}

.calendar-grid {
  display: flex;
  gap: 3px;
  overflow-x: auto;
  padding: 0.5rem 0;
}

.calendar-week {
  display: flex;
  flex-direction: column;
  gap: 3px;
}

.calendar-day {
  width: 12px;
  height: 12px;
  border-radius: 2px;
  transition: all 0.2s ease;
  cursor: pointer;
}

.calendar-day.level-0 {
  background: rgba(48, 54, 61, 0.5);
}

.calendar-day.level-1 {
  background: rgba(14, 104, 68, 0.6);
}

.calendar-day.level-2 {
  background: rgba(0, 138, 92, 0.7);
}

.calendar-day.level-3 {
  background: rgba(38, 166, 91, 0.8);
}

.calendar-day.level-4 {
  background: rgba(57, 211, 83, 1);
}

.calendar-day:hover {
  transform: scale(1.3);
  box-shadow: 0 0 8px currentColor;
}

.calendar-legend {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  margin-top: 1rem;
  justify-content: flex-end;
  font-size: 0.75rem;
  color: #8b949e;
}

.legend-levels {
  display: flex;
  gap: 3px;
}

.legend-level {
  width: 12px;
  height: 12px;
  border-radius: 2px;
}

/* 热门文章 */
.blog-list {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.blog-item {
  padding: 1.5rem;
  background: rgba(13, 17, 23, 0.6);
  border: 1px solid transparent;
  border-radius: 8px;
  transition: all 0.3s ease;
  cursor: pointer;
}

.blog-item:hover {
  background: rgba(88, 166, 255, 0.05);
  border-color: #58a6ff;
  transform: translateX(5px);
}

.blog-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  gap: 1rem;
  margin-bottom: 0.75rem;
}

.blog-title {
  font-size: 1.1rem;
  font-weight: 600;
  color: #c9d1d9;
  margin: 0;
  flex: 1;
}

.blog-item:hover .blog-title {
  color: #58a6ff;
}

.blog-date {
  font-size: 0.875rem;
  color: #8b949e;
  white-space: nowrap;
}

.blog-excerpt {
  color: #8b949e;
  font-size: 0.95rem;
  line-height: 1.6;
  margin: 0 0 1rem 0;
  display: -webkit-box;
  -webkit-line-clamp: 2;
  line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

.blog-meta {
  display: flex;
  align-items: center;
  gap: 1rem;
  flex-wrap: wrap;
}

.meta-item {
  display: flex;
  align-items: center;
  gap: 0.25rem;
  color: #8b949e;
  font-size: 0.875rem;
}

.meta-item svg {
  flex-shrink: 0;
}

.blog-tags {
  display: flex;
  gap: 0.5rem;
  margin-left: auto;
}

.blog-tag {
  padding: 0.25rem 0.75rem;
  background: rgba(88, 166, 255, 0.1);
  border-radius: 12px;
  color: #58a6ff;
  font-size: 0.8rem;
}

/* 响应式 */
@media (max-width: 1024px) {
  .profile-container {
    grid-template-columns: 1fr;
  }
  
  .profile-sidebar {
    order: 2;
  }
  
  .profile-main {
    order: 1;
  }
}

@media (max-width: 768px) {
  .profile-container {
    padding: 0 1rem;
  }
  
  .avatar-wrapper {
    width: 150px;
    height: 150px;
  }
  
  .username {
    font-size: 1.5rem;
  }
  
  .calendar-grid {
    overflow-x: scroll;
  }
}

/* ========== 头像上传弹窗 ========== */
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.7);
  backdrop-filter: blur(4px);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
  animation: fadeIn 0.3s ease;
}

.modal-content {
  background: #161b22;
  border: 1px solid #30363d;
  border-radius: 12px;
  width: 90%;
  max-width: 500px;
  max-height: 90vh;
  overflow-y: auto;
  box-shadow: 0 20px 60px rgba(0, 0, 0, 0.5);
  animation: slideUp 0.3s ease;
}

.modal-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 1.5rem;
  border-bottom: 1px solid #30363d;
}

.modal-header h3 {
  font-size: 1.25rem;
  font-weight: 600;
  color: #c9d1d9;
  margin: 0;
  display: flex;
  align-items: center;
  gap: 0.75rem;
}

.modal-header h3 svg {
  color: #58a6ff;
}

.modal-close {
  width: 32px;
  height: 32px;
  border-radius: 6px;
  background: transparent;
  border: none;
  color: #8b949e;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  transition: all 0.2s ease;
}

.modal-close:hover {
  background: rgba(88, 166, 255, 0.1);
  color: #58a6ff;
}

.modal-body {
  padding: 1.5rem;
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

.upload-tips {
  display: flex;
  gap: 1rem;
  padding: 1rem;
  background: rgba(88, 166, 255, 0.05);
  border: 1px solid rgba(88, 166, 255, 0.2);
  border-radius: 8px;
}

.upload-tips svg {
  flex-shrink: 0;
  color: #58a6ff;
  margin-top: 2px;
}

.tips-text {
  flex: 1;
}

.tips-text p {
  margin: 0.25rem 0;
  color: #8b949e;
  font-size: 0.875rem;
  line-height: 1.5;
}

.upload-confirm-btn {
  width: 100%;
  padding: 0.875rem 1.5rem;
  background: linear-gradient(135deg, #58a6ff 0%, #1f6feb 100%);
  border: none;
  border-radius: 8px;
  color: white;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 0.5rem;
  transition: all 0.3s ease;
}

.upload-confirm-btn:hover:not(:disabled) {
  transform: translateY(-2px);
  box-shadow: 0 8px 20px rgba(88, 166, 255, 0.4);
}

.upload-confirm-btn:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.spinner {
  width: 16px;
  height: 16px;
  border: 2px solid rgba(255, 255, 255, 0.3);
  border-top-color: white;
  border-radius: 50%;
  animation: spin 0.6s linear infinite;
}

@keyframes fadeIn {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}

@keyframes slideUp {
  from {
    opacity: 0;
    transform: translateY(20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

@keyframes spin {
  to {
    transform: rotate(360deg);
  }
}
</style>
